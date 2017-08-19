using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	int message = 0;

    // Use this for initialization
    void Start ()
    {
        status = CharacterStatus.Normal;
	}
	
    public void DecreseNakedGage()
    {
        m_nakedGage -= 0.2f * Time.deltaTime; //0.05f
		//UI Code - text box
		/*if (m_nakedGage > 0.75) 
		{
			セリフのチョイスの一つ
		}
		else if (m_nakedGage > 0.5 && m_nakedGage < 0.74) 
		{
		}
		else if(m_nakedGage > 0.25 && m_nakedGage < 0.49) 
		{
		}
		(m_nakedGage > 0.01 && m_nakedGage< 0.24) */

        if(m_nakedGage<0)
        {
            //ゲームオーバー
            GameManager.I.GameClear();
            m_nakedGage = 0;
        }

    }


    public void SetStatus(CharacterStatus status)
    {
            this.status = status;
    }

    public void OnItemPick()
    {
        m_nakedGage = Mathf.Min(1.0f,m_nakedGage+0.2f);
    }

	// Update is called once per frame
	void Update ()
    {
        slider.value = m_nakedGage;
        switch (status)
        {
            case CharacterStatus.Normal:
                break;
            case CharacterStatus.Discovery:
                DecreseNakedGage();
                break;
        }
        
	}
}
