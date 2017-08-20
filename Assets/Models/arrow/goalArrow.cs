using UnityEngine;
using System.Collections;

public class goalArrow : MonoBehaviour {
	
	public GameObject goalobject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.gameObject.transform.LookAt(goalobject.gameObject.transform.position,Vector3.up);
	}
}
