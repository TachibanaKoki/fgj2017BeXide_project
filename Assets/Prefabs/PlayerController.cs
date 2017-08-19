using UnityEngine;
using System.Collections;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Fall,
    Land,   //着地
    Attack,
    Damage,
    Clamber
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController I;
    public delegate void OnAttack();
    public OnAttack AttackCallBack = null;

    /*パラメータ*/
    [SerializeField]
    float moveSpeed = 10;
    [SerializeField]
    float jumpPower = 250;
    [SerializeField]
    float airMoveSpeed = 5;
    public PlayerState currentState;  //現在のステート

    [SerializeField]
    private float MaxStamina;
    public float stamina;
    private bool isStaminaDepletion;

    GameObject cameraObject;
    Rigidbody body = null;
    public Vector3 movement { get; private set; }
    public Vector3 oldPosition { get; private set; }

    bool isSquat;

    private float jumpTime = 0;
    private float m_accel;
    public bool isJump;
    bool isLanding;

    public bool IsOnRotateIsrand = false;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        oldPosition = transform.position;
        currentState = PlayerState.Idle;
        cameraObject = GameObject.Find("MainCamera");
        stamina = MaxStamina;
        isStaminaDepletion = false;
        isSquat = false;
        m_accel = 0;
        isJump = false;
        isLanding = false;
    }

    void FixedUpdate()
    {
        Vector3 movement = GetInputVector();
        if (movement == Vector3.zero)
        {
            m_accel = 0.0f;
        }
        else
        {
            m_accel = movement.magnitude * 6.666f;
        }
        GetComponent<Animator>().SetFloat("RunSpeed", m_accel);
        //スタミナ
        stamina = Mathf.Min(MaxStamina, stamina + Time.deltaTime);
        if (stamina == MaxStamina)
            isStaminaDepletion = false;

        //弧を描くように移動
        Vector3 forward = Vector3.Slerp(
            transform.forward,  //正面から
            movement,          //入力の角度まで
            700 * Time.deltaTime / Vector3.Angle(transform.forward, movement)
            );
        transform.LookAt(transform.position + forward);

        transform.Translate(movement, Space.World);
        //body.AddForce(movement*50.0f,ForceMode.VelocityChange);
        this.movement = movement;
        oldPosition = transform.position;
    }

    void Update()
    {

        Vector3 movement = transform.position - oldPosition;
        jumpTime += Time.deltaTime;
        //Debug.Log("currentState:"+currentState);
        bool isButtonDown = (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space));

    }

    Vector3 GetInputVector()
    {
        if (currentState == PlayerState.Jump) return Vector3.zero;
        if (currentState == PlayerState.Clamber) return Vector3.zero;
        int hash = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (hash == Animator.StringToHash("Damage") || hash == Animator.StringToHash("LongJump") || hash == Animator.StringToHash("Clamber") || hash == Animator.StringToHash("ClamberFailed"))
        {
            return Vector3.zero;
        }

        Vector2 leftStick = Vector2.zero;

        if (leftStick == Vector2.zero)
        {
            if (Input.GetKey(KeyCode.A)) leftStick.x = -1;
            if (Input.GetKey(KeyCode.D)) leftStick.x = 1;
            if (Input.GetKey(KeyCode.W)) leftStick.y = 1;
            if (Input.GetKey(KeyCode.S)) leftStick.y = -1;
        }

        Vector3 movement = new Vector3(leftStick.x, 0, leftStick.y);

        Quaternion cameraRotation = cameraObject.transform.rotation;
        cameraRotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0);
        movement = cameraRotation * movement;

        if (currentState == PlayerState.Idle || currentState == PlayerState.Move)
        {
            //移動しているかしていないか
            currentState = movement == Vector3.zero ? PlayerState.Idle : PlayerState.Move;
        }

        if (currentState == PlayerState.Jump || currentState == PlayerState.Fall)
            return movement * airMoveSpeed;
        else
            return movement * moveSpeed;
    }


    bool IsOnGround(float distance = 0.3f)
    {
        RaycastHit hit;

        Ray underRay = new Ray(transform.position + (Vector3.up * 0.2f), Vector3.down);

        if (Physics.Raycast(underRay, out hit, distance))
        {
            if (hit.transform.tag != "Wall")
            {
                gameObject.transform.SetParent(hit.collider.transform);
                return true;
            }
        }
        gameObject.transform.parent = null;
        return false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (currentState == PlayerState.Jump)
        {
            if (IsOnGround())
            {
                currentState = PlayerState.Idle;
                return;
            }
        }
    }

    GameObject GetForwardObject()
    {
        RaycastHit hit;

        Ray underRay = new Ray(transform.position + Vector3.up * 1.5f, transform.forward);

        if (!Physics.Raycast(underRay, out hit, 1.0f))
        {
            return null;
        }

        if (hit.transform.tag == "Player")
        {
            return null;
        }

        return hit.collider.gameObject;
    }

    public void FowardForce()
    {
        StartCoroutine(FowardForceCourtine());
    }

    IEnumerator FowardForceCourtine()
    {
        float t = 0;
        while(true)
        {
            t += Time.deltaTime;
            transform.GetComponent<Rigidbody>().AddForce(transform.forward * 3000.0f*Time.deltaTime, ForceMode.Acceleration);
            if (t > 0.2f) break;
            yield return null;
        }
    }

}
