using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrees : MonoBehaviour
{

	public int mX;
	public int mY;
	public GameObject mTree;
	public Transform mTreeContainer;

	void Awake ()
	{
		Constants.NUM_X = mX;
		Constants.NUM_Y = mY;		
	}

	void Start ()
	{

		for (int i = 0; i < mX; i++) {
			for (int j = 0; j < mY; j++) {
				Instantiate (mTree, new Vector3 (i, 0f, j), Quaternion.identity, mTreeContainer);
			}
		}

		mTreeContainer.BroadcastMessage ("GetAllTreesAround", SendMessageOptions.DontRequireReceiver);
		
	}

}
