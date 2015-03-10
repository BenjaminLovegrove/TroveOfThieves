using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_EventManager : MonoBehaviour {

	s_TheifManager TheifManager;

	// Handles Player Turn
	enum EnumState {
		playerOne,
		playerTwo,
		movePhase,
	} EnumState playerTurn;

	// A way to allow other scripts to refrence above Enum
	public int playerTurnToken;

	//Handles Player Action Points
	public int playerOneAP, playerTwoAP;
	public Text playerOneGoldText, playerTwoGoldText;
	public Text playerOneTurnText, playerTwoTurnText;

	//Handles Some Audio
	AudioSource audioToPlay;
	public AudioClip p1MoveAudio;
	public AudioClip p2MoveAudio;

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
	}
	
	// Update is called once per frame
	void Update () {
		GoldUpdate ();
		PlayerTurnTextCheck ();
	}

	void GoldUpdate(){
		// Updates Player Gold Text
		playerOneGoldText.text = ("AP: " + playerOneAP);
		playerTwoGoldText.text = ("AP: " + playerTwoAP);
	}

	// Displays Player Turn
	// This May Be Grebox Only
	void PlayerTurnTextCheck(){
		if (playerTurn == EnumState.playerOne) {
			playerOneTurnText.text = "Your Turn!";
			playerTwoTurnText.text = "Your Turn!";
			playerOneTurnText.enabled = true;
			playerTwoTurnText.enabled = false;
			playerTurnToken = 1;
		}
		else if (playerTurn == EnumState.playerTwo){
			playerOneTurnText.text = "Your Turn!";
			playerTwoTurnText.text = "Your Turn!";
			playerOneTurnText.enabled = false;
			playerTwoTurnText.enabled = true;
			playerTurnToken = 2;
		}
		else if (playerTurn == EnumState.movePhase) {
			playerOneTurnText.text = "Theifes Turn!";
			playerTwoTurnText.text = "Moving x" + TheifManager.moveSteps + "Steps";
			playerOneTurnText.enabled = true;
			playerTwoTurnText.enabled = true;
			playerTurnToken = 3;
		}
	}

	public void EndTurnPressed(){
		// Checks Whos Turn It Is,
		// Updates Player Turn When Button Pressed
		if (playerTurn == EnumState.playerOne) {
			playerTurn = EnumState.playerTwo;
			playerOneTurnText.enabled = false;
			playerTwoTurnText.enabled = true;
			Camera.main.audio.clip = p2MoveAudio;
			Camera.main.audio.Play ();
		}
		else if (playerTurn == EnumState.playerTwo) {
			playerTurn = EnumState.movePhase;
			TheifManager.TheifTurnPhase ();
		}
		else if (playerTurn == EnumState.movePhase) {


			playerTurn = EnumState.playerOne;
			Camera.main.audio.clip = p1MoveAudio;
			Camera.main.audio.Play ();

		}
	}

	public void RollDicePressed(){
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
