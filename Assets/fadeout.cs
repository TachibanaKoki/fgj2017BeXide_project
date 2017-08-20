using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeout : MonoBehaviour {

	public Image image;
	// Use this for initialization
	void Start () {

		image.color = new Color (255, 255, 255, 255);
		image.CrossFadeAlpha (0, 3, false);
		StartCoroutine (FadeOut());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds (5.0f);
		image.CrossFadeAlpha (1, 3, false);
	}
}
	