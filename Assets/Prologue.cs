using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if ((Time.timeSinceLevelLoad > 4 || Input.GetButton ("Fire1")) && SceneManager.GetActiveScene ().buildIndex != 6) {			
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}

