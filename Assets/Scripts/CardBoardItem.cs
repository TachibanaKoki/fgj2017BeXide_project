using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardItem : MonoBehaviour
{
    public CardBoardParts partsType;
    [HideInInspector]
    public Material mat;
    

	void Start ()
    {
        mat = GetComponent<Renderer>().material;
        partsType = (CardBoardParts)Random.Range(0,4);
	}
	
	void Update ()
    {
		
	}
}
