using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_EventManager : MonoBehaviour {

	s_TheifManager TheifManager;
	GameObject[] destroyChecks;
	s_DiceRoll DiceRoller;

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
	public int dieOne, dieTwo;
	public int playerOneAP, playerTwoAP;
	public Text playerOneAPText, playerTwoAPText;
	public Text playerOneGoldGUI, playerTwoGoldGUI;
	public Text playerOneTurnText, playerTwoTurnText;

	public SpriteRenderer dieOneTexture, dieTwoTexture;
	public Sprite d1,d2,d3,d4,d5,d6;

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
		DiceRoller = this.gameObject.GetComponent<s_DiceRoll> ();
		//Sets To Default
		playerTurn = EnumState.playerOne;
		playerTurnToken = 1;
		Camera.main.audio.clip = p1MoveAudio;
		Camera.main.audio.Play ();
		RollDice ();
	}
	
	// Update is called once per frame
	void Update () {
		GoldUpdate ();
		PlayerTurnTextCheck ();
		playerOneGoldGUI.text = (playerOneGold + " Gold");
		playerTwoGoldGUI.text = (playerTwoGold + " Gold");
	}

	void RollDice(){
	//	Camera.main.audio.clip = rollDiceAudio;
	//	Camera.main.audio.Play ();
		dieOne = DiceRoller.DoRollOne ();
		dieTwo = DiceRoller.DoRollTwo ();
		playerOneAP = dieOne + dieTwo;
		playerTwoAP = dieOne + dieTwo;
		ChangeDieSprite ();

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
			playerOneTurnText.enabled = true;
			playerTwoTurnText.enabled = false;
		} else if (playerTurn == EnumState.playerTwo) {
			playerOneTurnText.text = "Your Turn!";
			playerTwoTurnText.text = "Your Turn!";
			playerTurnToken = 2;
			playerOneTurnText.enabled = false;
			playerTwoTurnText.enabled = true;
		} else if (playerTurn == EnumState.movePhase) {
		//	playerOneTurnText.text = "Move Phase";
		//	playerTwoTurnText.text = "Move Phase";
			playerTurnToken = 3;
			playerOneTurnText.enabled = true;
			playerTwoTurnText.enabled = true;
		}
	}

	public void EndTurnPressed(){
		// Checks Whos Turn It Is,
		// Updates Player Turn When Button Pressed
		if (playerTurn == EnumState.playerOne) {
			playerTurn = EnumState.playerTwo;
			Camera.main.audio.clip = p2MoveAudio;
			Camera.main.audio.Play ();
		}
		else if (playerTurn == EnumState.playerTwo) {
			playerTurn = EnumState.movePhase;
			TheifManager.TheifTurnPhase ();
			DestroyCheck();
		}
	}

	public void MovePhaseEnd(){
		playerTurn = EnumState.playerOne;
		Camera.main.audio.clip = p1MoveAudio;
		Camera.main.audio.Play ();
		RollDice ();
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

	void ChangeDieSprite(){
		if (dieOne == 1)
			dieOneTexture.sprite = d1;
		if (dieOne == 2)
			dieOneTexture.sprite = d2;
		if (dieOne == 3)
			dieOneTexture.sprite = d3;
		if (dieOne == 4)
			dieOneTexture.sprite = d4;
		if (dieOne == 5)
			dieOneTexture.sprite = d5;
		if (dieOne == 6)
			dieOneTexture.sprite = d6;
		if (dieTwo == 1)
			dieTwoTexture.sprite = d1;
		if (dieTwo == 2)
			dieTwoTexture.sprite = d2;
		if (dieTwo == 3)
			dieTwoTexture.sprite = d3;
		if (dieTwo == 4)
			dieTwoTexture.sprite = d4;
		if (dieTwo == 5)
			dieTwoTexture.sprite = d5;
		if (dieTwo == 6)
			dieTwoTexture.sprite = d6;
	}
}
