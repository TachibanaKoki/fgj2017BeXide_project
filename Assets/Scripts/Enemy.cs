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

	public TextMesh tm;

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

			if (isStop) { // 老人ならゲージによって台詞を変える。
				tm.text = selectSerifu (col.GetComponent<CharacterManager>());
			};
			tm.gameObject.SetActive (true);
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
			tm.gameObject.SetActive (false);
		}
	}

	// キャラクターを渡したら、Enemyに対応するセリフを返します。
	private string selectSerifu(CharacterManager cm){
		float m_nakedGage = cm.m_nakedGage;
		string serifu;
		int message = 0;
		message = Random.Range(1, 6);

		if(m_nakedGage > 0.01 && m_nakedGage< 0.24)
		{
			if (message == 1)
				serifu = "ヘンタイ！";
			else if (message == 2)
				serifu = "裸じゃねぇか？";
			else if (message == 3)
				serifu = "ここに変態がいるぞい！！";
			else if (message == 4)
				serifu = "やばい！不審者！";
			else if (message == 5)
				serifu = "ギャァァァァ！！";
			else
				serifu = "ワイジャパニーズピーポー？";
		}
		else if(m_nakedGage > 0.25 && m_nakedGage < 0.49) 
		{
			if (message == 1)
				serifu = "あれ？！";
			else if (message == 2)
				serifu = "Oh my God!!";
			else if (message == 3)
				serifu = "What the…?";
			else if (message == 4)
				serifu = "お兄さん、どうした？";
			else if (message == 5)
				serifu = "ほんまに？";
			else
				serifu = "一体だれ。。。？";
		}
		else if (m_nakedGage > 0.5 && m_nakedGage < 0.74) 
		{
			if (message == 1)
				serifu = "何それ？";
			else if (message == 2)
				serifu = "あのやつがあやしい。。。";
			else if (message == 3)
				serifu = "お前さん、何としとるんじゃ？";
			else if (message == 4)
				serifu = "えぇぇぇぇ、何？";
			else if (message == 5)
				serifu = "何じゃ？あの人影は。。。";
			else
				serifu = "。。。。。。。。？";
		}
		else
		{
			if (message == 1)
				serifu = "。。。まぁ、ファションかな？";
			else if (message == 2)
				serifu = "若者がわかりません。";
			else if (message == 3)
				serifu = "ララララララ。。。。";
			else if (message == 4)
				serifu = "今日もいい天気じゃ！";
			else if (message == 5)
				serifu = "あ、またわしの足が暴れとるわい。";
			else
				serifu = "家に帰って笑点でも見ようか？";
		}

		return serifu;
	}

}
