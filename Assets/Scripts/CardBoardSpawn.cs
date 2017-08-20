using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardSpawn : MonoBehaviour
{

    public GameObject CoardBoardPrefab;

    public int InstanceCount=300;

    public Material[] CardBoardMaterial;

	void Start ()
    {
		for(int i=0;i<InstanceCount;i++)
        {
            GameObject go = GameObject.Instantiate(CoardBoardPrefab, new Vector3(Random.Range(-200,300),30,Random.Range(-100,0)), Quaternion.identity);
            go.GetComponent<Renderer>().material = CardBoardMaterial[Random.Range(0, CardBoardMaterial.Length)];
        }
	}
	
	void Update ()
    {
		
	}
}
