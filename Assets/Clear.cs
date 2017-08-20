using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Clear : MonoBehaviour {

	[RequireComponent(typeof(AudioSource))]

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AudioSource audio = GetComponent<AudioSource>();
		if (Time.timeSinceLevelLoad > 4)
			//fade out
		if (Time.timeSinceLevelLoad == 7)
		if (Time.timeSinceLevelLoad == 21)
			SceneManager.LoadScene("Title");
	}
}
