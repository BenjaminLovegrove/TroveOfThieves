﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_TheifManager : MonoBehaviour {
	
	s_EventManager EventManager;

	GameObject[] theifSpawns;
	bool isMovementTurn;
	public int moveSteps;
	float moveWaitTime = 2;
	float moveTimer = 0;

	// Use this for initialization
	void Start () {
		EventManager = this.gameObject.GetComponent<s_EventManager> ();
		isMovementTurn = false;
		moveSteps = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMovementTurn) {
			moveWaitTime = moveSteps;
			moveTimer += Time.deltaTime;
			if (moveTimer >= moveWaitTime){
				EventManager.EndTurnPressed ();
				moveTimer = 0;
				isMovementTurn = false;
			}
		}
	}

	public void TheifTurnPhase(){
		moveSteps = Random.Range (2, 13);
		theifSpawns = GameObject.FindGameObjectsWithTag ("P1Theif");
		foreach (GameObject theifSpawn in theifSpawns) {
			theifSpawn.SendMessage ("DoMove", moveSteps);
		}
		theifSpawns = GameObject.FindGameObjectsWithTag ("P2Theif");
		foreach (GameObject theifSpawn in theifSpawns) {
			theifSpawn.SendMessage ("DoMove", -moveSteps);
		}
		isMovementTurn = true;
	}
}
