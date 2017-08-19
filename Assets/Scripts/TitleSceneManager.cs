using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    string SceneName = "main";

    bool isNext = false;
    AsyncOperation async;
    bool isBGMEvent = false;
	// Use this for initialization
	void Start ()
    {
		if(SoundManager.Instance==null)
        {
            async = SceneManager.LoadSceneAsync("SoundSetupScene",LoadSceneMode.Additive);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(async!=null&&async.isDone&&!isBGMEvent)
        {
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.title);
            isBGMEvent = true;
        }
		if(Input.GetButton("Fire1")&&!isNext)
        {
            isNext = true;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Additive);
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.bgm_low);
        }
	}
}
