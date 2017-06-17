using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	public GameObject mTreeContainer;

	[Header ("UI")]
	public GameObject panelGameStart;
	public Button btnStartGame;

	public GameObject panelInGame;
	public Text txtScoreInGame;

	public GameObject panelGameOver;
	public Text txtScore;
	public Button btnBack2Start;
	public Button btnRestart;

	void Start ()
	{
//		btnNextTurn.onClick.AddListener (() => {
//			Invoke ("StartNextTurn", 0f);
//			InvokeRepeating ("StartNextTurn", 2f, 2f);
//		});

//		btnAddTrees.onClick.AddListener (() => {
//
//			List<int> positions = Utils.GetRandomNumbers (Constants.NUM_X * Constants.NUM_Y, Constants.NUM_ADD);
//			for (int i = 0; i < positions.Count; i++) {
//				print (positions [i]);
//				mTreeContainer.transform.GetChild (positions [i]).GetComponent<TreeLogic> ().TreeGrow ();
//			}
//
//		});

		//开始界面
		btnStartGame.onClick.AddListener (() => {
			panelGameStart.SetActive (false);
			panelInGame.SetActive (true);
			StartGame ();
		});

		//Game中

		//Game Over
		btnBack2Start.onClick.AddListener (() => {
			panelGameOver.SetActive (false);
			panelGameStart.SetActive (true);
		});
		btnRestart.onClick.AddListener (() => {
			panelInGame.SetActive (true);
			panelGameOver.SetActive (false);
			StartGame ();
		});
	}

	//	void Update ()
	//	{
	//
	//	}


	private void StartGame ()
	{
		//随机add树
		List<int> positions = Utils.GetRandomNumbers (Constants.NUM_X * Constants.NUM_Y, Constants.NUM_ADD);
		for (int i = 0; i < positions.Count; i++) {
			//				print (positions [i]);
			mTreeContainer.transform.GetChild (positions [i]).GetComponent<TreeLogic> ().TreeGrow ();
		}
		//开始循环
		InvokeRepeating ("StartNextTurn", 2f, 2f);
	}

	private void StartNextTurn ()
	{
		//先检查还有没有活着的
		bool hasAlive = false;
		foreach (var item in mTreeContainer.GetComponentsInChildren<TreeLogic>()) {
			if (item.mCurrentStatus != TreeStatus.Dead) {
				hasAlive = true;
				break;
			}
		}
		//
		if (hasAlive) {
			TurnManager.Instance.NextTurn ();
			txtScoreInGame.text = Constants.TURN_COUNT.ToString ();
		} else {
			print ("世界消亡");
			CancelInvoke ("StartNextTurn");

			panelGameOver.SetActive (true);
			txtScore.text = Constants.TURN_COUNT.ToString ();
		}
	}
}
