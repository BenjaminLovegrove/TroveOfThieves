using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_ButtonUI : MonoBehaviour {

	s_EventManager EventManager;
	bool rolledDice = false;

	public Button rollDiceButton, endTurnButton;

	public Texture atk1, atk2, atk3, atk4, atk5, atk6;
	public Texture def1, def2, def3, def4, def5, def6;
	public GameObject atkOBJ1, atkOBJ2;
	public GameObject defOBJ1, defOBJ2;
	Vector3 objSpawn = new Vector3(100, 100, 100);

	// Use this for initialization
	void Start () {
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
		GetGuiElements ();
	}

	void GetGuiElements(){
		//rollDice = GetComponent<Button> ();

	}
	
	// Update is called once per frame
	void Update () {
		UnityGuiController ();
	}

	void OnGUI(){
		if(EventManager.playerTurnToken != 3)
			SpellsButtonGUI ();
	}
	/* This will handle weather or not a button is visible/active or not */
	void UnityGuiController(){
		if (!rolledDice && EventManager.playerTurnToken != 3) {
			rollDiceButton.gameObject.SetActive (true);
		} else { rollDiceButton.gameObject.SetActive (false); }

		if (EventManager.playerTurnToken != 3) {
			endTurnButton.gameObject.SetActive (true);
		} else { endTurnButton.gameObject.SetActive (false); }

	}

	/*Upon a UI button being press, the fuction will be called
	 * A function is then called for simple UI and Spell items based on message string */
	public void UnityUI(string buttonName){
		TurnControllingUI (buttonName);
	}

	/* This part will handle the Roll Dice and End Turn Buttons */
	void TurnControllingUI(string buttonName){
		if (buttonName == "RollDice") {
			rolledDice = true;
			EventManager.RollDicePressed ();
		}
		if (buttonName == "EndTurn") {
			EventManager.EndTurnPressed ();
			rolledDice = false;
		}
	}


	//Soon to be depreciated and converted to UI system
	void SpellsButtonGUI(){
		//Def Spells
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 6, Screen.height / 2 - 37.5f, 75, 75), atk1)) {
			Instantiate (atkOBJ1, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 5, Screen.height / 2 - 37.5f, 75, 75), atk2)) {
			Instantiate (atkOBJ2, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 4, Screen.height / 2 - 37.5f, 75, 75), atk3)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 3, Screen.height / 2 - 37.5f, 75, 75), atk4)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 2, Screen.height / 2 - 37.5f, 75, 75), atk5)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85, Screen.height / 2 - 37.5f, 75, 75), atk6)) {
			
		}
		
		//Atk Spells
		if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.height / 2 - 37.5f, 75, 75), def1)) {
			Instantiate (defOBJ1, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 1 + 10, Screen.height / 2 - 37.5f, 75, 75), def2)) {
			Instantiate (defOBJ2, objSpawn, defOBJ2.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 2 + 10, Screen.height / 2 - 37.5f, 75, 75), def3)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 3 + 10, Screen.height / 2 - 37.5f, 75, 75), def4)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 4 + 10, Screen.height / 2 - 37.5f, 75, 75), def5)) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 5 + 10, Screen.height / 2 - 37.5f, 75, 75), def6)) {
			
		}
		
	}
}
