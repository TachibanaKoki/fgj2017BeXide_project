using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public bool isGameClear = false;
    public void Start()
    {
        I = this;
        
        if (SoundManager.Instance==null)
        {
            SceneManager.LoadSceneAsync("SoundSetupScene", LoadSceneMode.Additive);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("main"));
        SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.bgm_low);
    }

	public void GameClear()
    {
        isGameClear = true;
        //とりあえず仮でシーンをループ
        SceneManager.LoadSceneAsync("main");
        SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.clear);
    }

    public void GameOver()
    {
        isGameClear = true;
        //とりあえず仮でシーンをループ
        SceneManager.LoadSceneAsync("Game Over");
        SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.gameover);
    }
}
