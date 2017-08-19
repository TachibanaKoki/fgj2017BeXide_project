using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardBoardParts
{
    Head,
    Body,
    Hip,
    LeftArm,
    RightArm,
}


public class CardboardController : MonoBehaviour
{
    [Tooltip("Head,body,hip,leftarm,rightarm")]
    public GameObject[] Parts;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "CardBoard")
        {
            PickCardBoard(col.gameObject.GetComponent<CardBoardItem>());
            Destroy(col.gameObject);
        }
    }


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit");
        if (col.tag == "Finish")
        {
            Debug.Log("TagHit");
            GameManager.I.GameClear();
        }
    }

    void PickCardBoard(CardBoardItem item)
    {
        Parts[(int)item.partsType].GetComponent<Renderer>().material.mainTexture = item.texture;
    }
}
