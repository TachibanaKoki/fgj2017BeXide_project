using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public enum CharacterStatus
{
    Normal,
    Discovery,

}


public class CharacterManager : SingletonMonoBehaviourFast<CharacterManager>
{
    public Slider slider;
    public CharacterStatus status;
	public float m_nakedGage = 1.0f;
    ThirdPersonUserControl TPuserControl;
    ThirdPersonUserControl.MoveState oldMoveState;
    

    // Use this for initialization
    void Start ()
    {
        status = CharacterStatus.Normal;
        TPuserControl = GetComponent<ThirdPersonUserControl>();
        oldMoveState = TPuserControl.moveState;
	}
	
    public void DecreseNakedGage()
    {
        m_nakedGage -= 0.4f * Time.deltaTime; //0.05f

        if(m_nakedGage<0)
        {
            //ゲームオーバー
            GameManager.I.GameOver();
            m_nakedGage = 0;
        }

    }


    public void SetStatus(CharacterStatus status)
    {
            this.status = status;
    }

    public void OnItemPick()
    {
        m_nakedGage = Mathf.Min(1.0f,m_nakedGage+0.15f);
    }

	// Update is called once per frame
	void Update ()
    {
        m_nakedGage -= 0.03f * Time.deltaTime;
        slider.value = m_nakedGage;
        switch (status)
        {
            case CharacterStatus.Normal:
                break;
            case CharacterStatus.Discovery:
                DecreseNakedGage();
                break;
        }

        TPuserControl.isClear = GameManager.I.isGameClear;

        if(oldMoveState != TPuserControl.moveState)
        {
            if(TPuserControl.moveState == ThirdPersonUserControl.MoveState.Run)
            {
                SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.Footstep);
            }
            else
            {
                SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.Standing);
            }
        }
        oldMoveState = TPuserControl.moveState;


    }
}
