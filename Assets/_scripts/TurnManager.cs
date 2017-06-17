using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public static TurnManager Instance {
		private set;
		get;
	}

	private int mTurnCount;
	public GameObject mTreeContainer;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	public void NextTurn ()
	{
		StartCoroutine (NextTurnCoroutine ());
	}

	IEnumerator NextTurnCoroutine ()
	{
		mTurnCount++;
		Constants.TURN_COUNT = mTurnCount;
		mTreeContainer.BroadcastMessage ("GetAliveAround", SendMessageOptions.DontRequireReceiver);//可以考虑协程循环发出
		yield return new WaitForSeconds (0.5f);
		mTreeContainer.BroadcastMessage ("RefreshTreeStatus", SendMessageOptions.DontRequireReceiver);
	}
}
