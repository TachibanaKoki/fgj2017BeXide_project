using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointsData : MonoBehaviour {

    public static NavPointsData I;

    public Transform[] points;

    // Use this for initialization
    void Awake ()
    {
        points = transform.GetComponentsInChildren<Transform>();
        I = this;
	}
}
