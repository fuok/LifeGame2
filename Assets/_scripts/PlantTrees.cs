using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrees : MonoBehaviour
{

	public int mX;
	public int mY;
	public int mAddNum;
	//为mX*mY的1/14～1/15

	[Header ("生长因子/消亡因子")]
	public int mThresholdGrow;
	public int mThresholdDieMin;
	public int mThresholdDieMax;

	public GameObject mTreeNode;
	public Transform mTreeContainer;

	void Awake ()
	{
		Constants.NUM_X = mX;
		Constants.NUM_Y = mY;	
		Constants.NUM_ADD = mAddNum;
		Constants.THRESHOLD_GROW = mThresholdGrow;
		Constants.THRESHOLD_DIE_MIN = mThresholdDieMin;
		Constants.THRESHOLD_DIE_MAX = mThresholdDieMax;
	}

	void Start ()
	{

		for (int i = 0; i < mX; i++) {
			for (int j = 0; j < mY; j++) {
				GameObject node = Instantiate (mTreeNode, new Vector3 (i, 0f, j), Quaternion.identity, mTreeContainer);

				//添加树模型
				Instantiate (Resources.Load<GameObject> ("tree/Tree-01"), node.transform);
			}
		}

		mTreeContainer.BroadcastMessage ("GetAllTreesAround", SendMessageOptions.DontRequireReceiver);
		
	}

}
