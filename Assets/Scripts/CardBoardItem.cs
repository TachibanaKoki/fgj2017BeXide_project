using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardItem : MonoBehaviour
{
    public CardBoardParts partsType;
    [HideInInspector]
    public Texture texture;

	void Start ()
    {
        texture = GetComponent<Renderer>().material.mainTexture;
	}
	
	void Update ()
    {
		
	}
}
