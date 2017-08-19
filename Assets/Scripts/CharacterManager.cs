using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStatus
{
    Normal,
    Discovery,

}


public class CharacterManager : MonoBehaviour
{
    public Slider slider;
    public CharacterStatus status;
    float m_nakedGage = 1.0f;

    // Use this for initialization
    void Start ()
    {
        status = CharacterStatus.Normal;
	}
	
    public void DecreseNakedGage()
    {
        m_nakedGage -= 0.2f * Time.deltaTime;

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
