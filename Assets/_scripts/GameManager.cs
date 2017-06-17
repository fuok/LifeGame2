using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Button btnNextTurn;
	public Button btnAddTrees;
	public GameObject mTreeContainer;

	void Start ()
	{
		btnNextTurn.onClick.AddListener (() => {
//			mTreeContainer.BroadcastMessage ("GetAliveAround", SendMessageOptions.RequireReceiver);//可以考虑协程循环发出
//			foreach (var item in mTreeContainer.GetComponentsInChildren<TreeLogic>()) {
//				item.BroadcastMessage
//			}
//			Invoke ("StartNextTurn", 0f);
			InvokeRepeating ("StartNextTurn", 0.5f, 1f);


		});

		btnAddTrees.onClick.AddListener (() => {

			List<int> positions = Utils.GetRandomNumbers (Constants.NUM_X * Constants.NUM_Y, Constants.NUM_ADD);
			for (int i = 0; i < positions.Count; i++) {
//				print (positions [i]);
				mTreeContainer.transform.GetChild (positions [i]).GetComponent<TreeLogic> ().TreeGrow ();
			}

		});
	}

	void Update ()
	{
		
	}

	private void StartNextTurn ()
	{
		bool hasAlive = false;
		foreach (var item in mTreeContainer.GetComponentsInChildren<TreeLogic>()) {
			if (item.mCurrentStatus != TreeStatus.Dead) {
				hasAlive = true;
				break;
			}
		}
		if (hasAlive) {
			TurnManager.Instance.NextTurn ();
		} else {
			print ("世界消亡");
			CancelInvoke ("StartNextTurn");
		}
	}
}
