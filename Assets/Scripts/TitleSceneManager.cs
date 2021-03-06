﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    string SceneName = "Prologue 1";

    bool isNext = false;
    AsyncOperation async;
    bool isBGMEvent = false;
	// Use this for initialization
	void Start ()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        if (SoundManager.Instance==null)
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
        else if(SoundManager.Instance!=null&& !isBGMEvent)
        {
            SoundManager.Instance.SoundEvent(SoundManager.EnumBgmEvent.title);
            isBGMEvent = true;
        }
		if(Input.GetButton("Fire1")&&!isNext)
        {
			{
				SoundManager.Instance.SoundEvent (SoundManager.EnumBgmEvent.opening);
			}
            isNext = true;
            SceneManager.LoadScene(SceneName);

        }
	}
}
