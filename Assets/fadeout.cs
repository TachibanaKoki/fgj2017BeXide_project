using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeout : MonoBehaviour {

	public Image image;
	float alp = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		image.color = new Color (0, 0, 0, alp);
		alp += 5;
		if (alp == 255) {
			alp -= 10;
		}
		
	}
}
