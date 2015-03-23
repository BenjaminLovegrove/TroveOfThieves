using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class s_TheifManager : MonoBehaviour {
	
	s_EventManager EventManager;
	s_ButtonUI ButtonUI;

	GameObject[] theifSpawns;
	bool isMovementTurn;
	public int moveSteps;
	float moveWaitTime = 2;
	float moveTimer = 0;

	// Use this for initialization
	void Start () {
		EventManager = this.gameObject.GetComponent<s_EventManager> ();
		ButtonUI = (s_ButtonUI)GameObject.Find ("UI Camera").GetComponent<s_ButtonUI> ();
		isMovementTurn = false;
		moveSteps = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMovementTurn) {
			moveWaitTime = moveSteps;
			moveTimer += 2 * Time.deltaTime;
			if (moveTimer >= moveWaitTime) {
				moveTimer = 0;
				isMovementTurn = false;
				EventManager.MovePhaseEnd();
				ButtonUI.endTurnButton.gameObject.SetActive(true);
			} else {
				ButtonUI.endTurnButton.gameObject.SetActive(false);
			}
		}
	}

	public void TheifTurnPhase(){
		moveSteps = EventManager.playerOneAP;
		EventManager.playerOneTurnText.text = "Moving x" + EventManager.playerOneAP + " Steps";
		theifSpawns = GameObject.FindGameObjectsWithTag ("P1Theif");
		foreach (GameObject theifSpawn in theifSpawns) {
			theifSpawn.SendMessage ("DoMove", moveSteps, SendMessageOptions.DontRequireReceiver);
		}

		moveSteps = EventManager.playerTwoAP;
		EventManager.playerTwoTurnText.text = "Moving x" + EventManager.playerTwoAP + " Steps";
		theifSpawns = GameObject.FindGameObjectsWithTag ("P2Theif");
		foreach (GameObject theifSpawn in theifSpawns) {
			theifSpawn.SendMessage ("DoMove", -moveSteps, SendMessageOptions.DontRequireReceiver);
		}

		EventManager.playerOneAP = 0;
		EventManager.playerTwoAP = 0;
		isMovementTurn = true;
	}
}
