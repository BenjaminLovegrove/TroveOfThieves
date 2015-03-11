using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_WinManager : MonoBehaviour {

	s_EventManager EventManager;
	Text winText;
	int playerOneGold, playerTwoGold;
	string winnerString;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		if (Application.loadedLevel == 1)
			EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();

		if (Application.loadedLevel == 2)
			print ("Hello");
			winText = (Text)GameObject.Find ("Canvas").GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 0)
			Destroy (this.gameObject);

		if (Application.loadedLevel == 1)
			LevelOne ();

		if (Application.loadedLevel == 2)
			LevelTwo ();
	}

	void LevelOne(){
		playerOneGold = EventManager.playerOneGold;
		playerTwoGold = EventManager.playerTwoGold;

		if (playerOneGold <= 0) {
			winnerString = "Player Two Is The Winner!";
			Application.LoadLevel (2);
		}
		if (playerTwoGold <= 0) {
			winnerString = "Player One Is The Winner!";
			Application.LoadLevel (2);
		}
	}

	void LevelTwo(){
		winText = (Text)GameObject.Find ("Canvas").GetComponentInChildren<Text> ();
		winText.text = winnerString;
	}
}
