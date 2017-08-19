using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    CharacterStatus state;

    GameObject character;
    
    public bool isStop = false;

    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent m_agent;

    // Use this for initialization
    void Start ()
    {
        state = CharacterStatus.Normal;
        if(!isStop)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
        points = NavPointsData.I.points;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case CharacterStatus.Normal:
                if(!isStop)
                {
                    if (m_agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }

                break;
            case CharacterStatus.Discovery:
                if (isStop)
                {
                    transform.LookAt(character.transform);
                }
                else
                {
                    m_agent.destination = character.transform.position;
                }

                break;
        }

	}

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (NavPointsData.I.points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        m_agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<CharacterManager>().SetStatus(CharacterStatus.Discovery);
            state = CharacterStatus.Discovery;
            character = col.gameObject;
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.bgm_hi);
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<CharacterManager>().SetStatus(CharacterStatus.Normal);
            state = CharacterStatus.Normal;
            if (!isStop)
            {
                GotoNextPoint();
            }
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.bgm_low);
        }
    }

}
