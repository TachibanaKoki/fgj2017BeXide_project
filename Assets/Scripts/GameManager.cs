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
    }

	public void GameClear()
    {
        isGameClear = true;
        //とりあえず仮でシーンをループ
        SceneManager.LoadSceneAsync("main");
    }
}
