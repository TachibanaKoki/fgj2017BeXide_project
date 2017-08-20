using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.timeSinceLevelLoad > 4 && SceneManager.GetActiveScene ().buildIndex != 8)
			SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex + 1);
		else if (SceneManager.GetActiveScene ().buildIndex == 9 && Input.GetButton ("Fire1"))
			SceneManager.LoadScene("Title");
	}
}
