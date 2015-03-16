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

	public Text moveText;

	// Use this for initialization
	void Start () {
		EventManager = this.gameObject.GetComponent<s_EventManager> ();
		ButtonUI = (s_ButtonUI)GameObject.Find ("UI Camera").GetComponent<s_ButtonUI> ();
		isMovementTurn = false;
		moveSteps = 0;
		moveText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMovementTurn) {
			moveText.text = "Movement Roll - " + moveSteps;
			moveText.enabled = true;
			moveWaitTime = moveSteps;
			moveTimer += Time.deltaTime;
			ButtonUI.EndButtonTimed (false);
			if (moveTimer >= moveWaitTime) {
				//EventManager.EndTurnPressed ();
				ButtonUI.EndButtonTimed (true);
				moveTimer = 0;
				isMovementTurn = false;
				moveText.enabled = false;
				if (EventManager.playerTurnToken == 1) {
					EventManager.playerOneTurnText.enabled = true;
					EventManager.playerTwoTurnText.enabled = false;
					Camera.main.audio.clip = EventManager.p1MoveAudio;
					Camera.main.audio.Play ();
				}
				if (EventManager.playerTurnToken == 2) {
					EventManager.playerOneTurnText.enabled = false;
					EventManager.playerTwoTurnText.enabled = true;
					Camera.main.audio.clip = EventManager.p2MoveAudio;
					Camera.main.audio.Play ();
				}
			}
		}
	}

	public void TheifTurnPhase(){
		moveSteps = Random.Range (2, 12);
		if (EventManager.playerTurnToken == 1) {
			theifSpawns = GameObject.FindGameObjectsWithTag ("P1Theif");
			foreach (GameObject theifSpawn in theifSpawns) {
				theifSpawn.SendMessage ("DoMove", moveSteps, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (EventManager.playerTurnToken == 2) {
			theifSpawns = GameObject.FindGameObjectsWithTag ("P2Theif");
			foreach (GameObject theifSpawn in theifSpawns) {
				theifSpawn.SendMessage ("DoMove", -moveSteps, SendMessageOptions.DontRequireReceiver);
			}
		}
		isMovementTurn = true;
	}
}
