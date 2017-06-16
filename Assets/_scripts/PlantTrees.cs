using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrees : MonoBehaviour
{

	public int mX;
	public int mY;
	public GameObject mTree;
	public Transform mTreeContainer;

	// Use this for initialization
	void Start ()
	{

		for (int i = 0; i < mX; i++) {
			for (int j = 0; j < mY; j++) {
				Instantiate (mTree, new Vector3 (i, 0f, j), Quaternion.identity, mTreeContainer);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
