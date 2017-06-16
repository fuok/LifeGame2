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
			Invoke ("StartNextTurn", 0f);
//			InvokeRepeating ("StartNextTurn", 1f, 2f);


		});

		btnAddTrees.onClick.AddListener (() => {

			List<int> positions = Utils.GetRandomNumbers (100, 20);
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
		TurnManager.Instance.NextTurn ();
	}
}
