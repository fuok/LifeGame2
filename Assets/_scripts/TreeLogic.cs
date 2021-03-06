﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum TreeStatus
{
	Alive,
	Young,
	Old,
	Dead
}

public class TreeLogic : MonoBehaviour
{
	public List<GameObject> mAliveAroundList;
	public int mAliveAround;
	public TreeStatus mCurrentStatus;
	//	public TreeStatus mNextStatus;

	public Material mMatBlack, mMatGreen, mMatYellow, mMatBrown;

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	//获取当前树周围的8棵树对象，游戏开始时通过消息调用一次
	private void GetAllTreesAround ()
	{
		StartCoroutine (GetAllTreesAroundCoroutine ());
	}

	IEnumerator GetAllTreesAroundCoroutine ()
	{
		//		int count = 0;
		for (int i = 0; i < 8; i++) {
			yield return new WaitForFixedUpdate ();
			Ray ray = new Ray (transform.position, Quaternion.Euler (0f, 45f * i, 0f) * Vector3.forward);
			RaycastHit hit;
			Physics.Raycast (ray, out hit, 1f);
			if (hit.collider) {
//				print ("打到了：" + hit.collider.name);//ok
				//				if (hit.collider.gameObject.GetComponent<TreeLogic> ().mCurrentStatus == TreeStatus.Alive) {
				//					count++;
				//				}
				mAliveAroundList.Add (hit.collider.gameObject);
			}
		}
		//		mAliveAround = count;
	}

	//---------------------PUBLIC-----------------------------------------------------------

	public void TreeGrow ()
	{
		mCurrentStatus = TreeStatus.Alive;
		transform.GetChild (0).gameObject.SetActive (true);
		//动画模式
//		transform.GetChild (0).gameObject.GetComponent<DOTweenAnimation> ().DOPlayById ("0");
		transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("toBig");
		//静态模式
		gameObject.GetComponentInChildren<Renderer> ().material = mMatGreen;
	}

	public void TreeOld ()
	{
		switch (mCurrentStatus) {
		case TreeStatus.Alive:
			mCurrentStatus = TreeStatus.Young;
			gameObject.GetComponentInChildren<Renderer> ().material = mMatYellow;
			break;
		case TreeStatus.Young:
			mCurrentStatus = TreeStatus.Old;
			gameObject.GetComponentInChildren<Renderer> ().material = mMatBrown;
			break;
		case TreeStatus.Old:
			TreeDie ();
			break;
		default:
			break;
		}
	}

	private void TreeDie ()
	{
		mCurrentStatus = TreeStatus.Dead;
		//动画模式
//		transform.GetChild (0).gameObject.GetComponent<DOTweenAnimation> ().DOPlayById ("1");
		transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("toSmall");
		//静态模式
//		transform.GetChild (0).gameObject.SetActive (false);
	}

	//获取当前树周围活着的树
	public void GetAliveAround ()
	{
		int count = 0;
		for (int i = 0; i < mAliveAroundList.Count; i++) {
//			if (mAliveAroundList [i].GetComponent<TreeLogic> ().mCurrentStatus == TreeStatus.Alive || mAliveAroundList [i].GetComponent<TreeLogic> ().mCurrentStatus == TreeStatus.Young) {
			if (mAliveAroundList [i].GetComponent<TreeLogic> ().mCurrentStatus != TreeStatus.Dead) {
				count++;
			}
		}
		mAliveAround = count;
	}

	//更新树的状态
	//		当前细胞为死亡状态时，当周围有3个存活细胞时，该细胞变成存活状态。 （模拟繁殖）
	//		当前细胞为存活状态时，当周围低于2个（不包含2个）存活细胞时， 该细胞变成死亡状态。（模拟生命数量稀少）
	//		当前细胞为存活状态时，当周围有2个或3个存活细胞时， 该细胞保持原样。
	//		当前细胞为存活状态时，当周围有3个以上的存活细胞时，该细胞变成死亡状态。（模拟生命数量过多）
	private void RefreshTreeStatus ()
	{
		switch (mCurrentStatus) {
		case TreeStatus.Dead:
			if (mAliveAround == Constants.THRESHOLD_GROW) {
				TreeGrow ();
			}
			break;
		case TreeStatus.Alive:
		case TreeStatus.Young:
		case TreeStatus.Old:
			//首先是自然衰亡
			TreeOld ();
			//之后是环境衰亡
			if (mAliveAround < Constants.THRESHOLD_DIE_MIN) {
				TreeOld ();
			} else if (mAliveAround > Constants.THRESHOLD_DIE_MAX) {
				TreeOld ();
			}
			break;
		default:
			break;
		}
	}


	void OnDrawGizmos ()
	{
//		print ("haha");
//		for (int i = 0; i < 8; i++) {
//			Gizmos.DrawRay (transform.position, Quaternion.Euler (0f, 45f, 0f) * Vector3.forward);
//		}
	}
}
