using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //AudioSource audio = GetComponent<AudioSource>();
        if (Time.timeSinceLevelLoad >= 21 || Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Title");
        }
	}
}
