﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_EventManager : MonoBehaviour {

	s_TheifManager TheifManager;
	GameObject[] destroyChecks;

	// Handles Player Turn
	enum EnumState {
		playerOne,
		playerTwo,
		movePhase,
	} EnumState playerTurn;

	// A way to allow other scripts to refrence above Enum
	public int playerTurnToken;

	//Handles Player Action Points
	public int playerOneGold = 1000;
	public int playerTwoGold = 1000;
	public int playerOneAP, playerTwoAP;
	public Text playerOneAPText, playerTwoAPText;
	public Text playerOneGoldGUI, playerTwoGoldGUI;
	public Text playerOneTurnText, playerTwoTurnText;

	//Handles Some Audio
	AudioSource audioToPlay;
	public AudioClip p1MoveAudio;
	public AudioClip p2MoveAudio;
	public AudioClip rollDiceAudio;

	void Awake(){
		PlayerTurnTextCheck ();
	}

	// Use this for initialization
	void Start () {
		Screen.fullScreen = true;
		TheifManager = this.gameObject.GetComponent<s_TheifManager> ();
		//Sets To Default
		playerTurn = EnumState.playerOne;
		playerOneAP = 0;
		playerTwoAP = 0;
		playerTurnToken = 1;
		Camera.main.audio.clip = p1MoveAudio;
		Camera.main.audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		GoldUpdate ();
		PlayerTurnTextCheck ();
		playerOneGoldGUI.text = (playerOneGold + " Gold");
		playerTwoGoldGUI.text = (playerTwoGold + " Gold");
	}

	void GoldUpdate(){
		// Updates Player Gold Text
		playerOneAPText.text = ("AP " + playerOneAP);
		playerTwoAPText.text = ("AP " + playerTwoAP);
	}

	// Displays Player Turn
	// This May Be Grebox Only
	void PlayerTurnTextCheck(){
		if (playerTurn == EnumState.playerOne) {
			playerOneTurnText.text = "Your Turn!";
			playerTwoTurnText.text = "Your Turn!";
			playerTurnToken = 1;
		}
		else if (playerTurn == EnumState.playerTwo){
			playerOneTurnText.text = "Your Turn!";
			playerTwoTurnText.text = "Your Turn!";
			playerTurnToken = 2;
		}
	}

	public void EndTurnPressed(){
		// Checks Whos Turn It Is,
		// Updates Player Turn When Button Pressed
		if (playerTurn == EnumState.playerOne) {
			TheifManager.TheifTurnPhase ();
			playerTurn = EnumState.playerTwo;
		}
		else if (playerTurn == EnumState.playerTwo) {
			TheifManager.TheifTurnPhase ();
			playerTurn = EnumState.playerOne;
			DestroyCheck();
		}
	}
	void DestroyCheck(){
		destroyChecks = GameObject.FindGameObjectsWithTag ("Boulder");
		foreach (GameObject destroyCheck in destroyChecks) {
			destroyCheck.SendMessage ("TurnCheck");
		}
		destroyChecks = GameObject.FindGameObjectsWithTag ("Barricade");
		foreach (GameObject destroyCheck in destroyChecks) {
			destroyCheck.SendMessage ("TurnCheck");
		}
		destroyChecks = GameObject.FindGameObjectsWithTag ("IceCube");
		foreach (GameObject destroyCheck in destroyChecks) {
			destroyCheck.SendMessage ("TurnCheck");
		}
	}

	public void RollDicePressed(){
		Camera.main.audio.clip = rollDiceAudio;
		Camera.main.audio.Play ();
		// Checks Whos Turn It Is,
		// Updates Player Gold When Button Pressed
		if (playerTurn == EnumState.playerOne) {
			playerOneAP += Random.Range (1,7);
			playerOneAP += Random.Range (1,7);
		}
		else if (playerTurn == EnumState.playerTwo) {
			playerTwoAP += Random.Range (1,7);
			playerTwoAP += Random.Range (1,7);
		}
	}

	public void InstantiateObject(string name){

	}
}
