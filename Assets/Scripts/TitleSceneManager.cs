using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    string SceneName = "main";

    bool isNext = false;

	// Use this for initialization
	void Start ()
    {
		if(SoundManager.Instance==null)
        {
            SceneManager.LoadSceneAsync("SoundSetupScene",LoadSceneMode.Additive);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

		if(Input.GetButton("Fire1")&&!isNext)
        {
            isNext = true;
            SceneManager.LoadSceneAsync(SceneName);
        }

	}
}
