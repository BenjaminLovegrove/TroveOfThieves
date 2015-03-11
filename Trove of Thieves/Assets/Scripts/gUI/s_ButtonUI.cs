using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_ButtonUI : MonoBehaviour {

	s_EventManager EventManager;
	bool rolledDice = false;

	public Button rollDiceButton, endTurnButton;

	public GameObject atkOBJ1, atkOBJ2;
	public GameObject defOBJ1, defOBJ2, defOBJ3, defOBJ4;
	Vector3 objSpawn = new Vector3(100, 100, 100);

	public GameObject theifOBJ;
	GameObject thiefSpawn;
	// Use this for initialization
	void Start () {
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
	}

	// Update is called once per frame
	void Update () {
		UnityGuiController ();
	}

	/* This will handle weather or not a button is visible/active or not */
	void UnityGuiController(){
		Text rollText = rollDiceButton.GetComponentInChildren<Text>();
		Text endText = endTurnButton.GetComponentInChildren<Text>();
		if (!rolledDice && EventManager.playerTurnToken != 3) {
			rollDiceButton.interactable = true;
			rollText.text = "Roll";
		} else { 
			rollDiceButton.interactable = false; 
			rollText.text = ". . .";
		}
		if (EventManager.playerTurnToken != 3) {
			endTurnButton.interactable = true;
			endText.text = "End";
		} else { 
			endTurnButton.interactable = false;
			endText.text = ". . .";
		}
	}

	/*Upon a UI button being press, the fuction will be called
	 * A function is then called for simple UI and Spell items based on message string */
	public void UnityUI(string buttonName){
		TurnControllingUI (buttonName);
		SpellButtonUI (buttonName);
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

	void SpellButtonUI(string buttonName){
		//Atttack Spells
		if (buttonName == "ATK1")
			Instantiate (atkOBJ1, objSpawn, defOBJ1.transform.rotation);
		if (buttonName == "ATK2")
			Instantiate (atkOBJ2, objSpawn, defOBJ1.transform.rotation);
		if (buttonName == "ATK3")
			SpawnThiefs ();

		//Defence Spells
		if (buttonName == "DEF1")
			Instantiate (defOBJ1, objSpawn, defOBJ1.transform.rotation);
		if (buttonName == "DEF2")
			Instantiate (defOBJ2, objSpawn, defOBJ1.transform.rotation);
		if (buttonName == "DEF3")
			Instantiate (defOBJ3, objSpawn, defOBJ1.transform.rotation);
		if (buttonName == "DEF4")
			Instantiate (defOBJ4, objSpawn, defOBJ1.transform.rotation);

	}

	void SpawnThiefs(){
		if (EventManager.playerTurnToken == 1) {
			Vector3 spawnRange = new Vector3(37, 3.2f,(Random.Range (-22,-36)));
			Vector3 hitPoint = new Vector3 (spawnRange.x, 6, spawnRange.z);
			RaycastHit hit;
			if (Physics.Raycast(hitPoint, -Vector3.up, out hit, 2.8f)){
				if (hit.collider.tag == "P2Theif"){
					print ("Already There");
				} 
			}else {
				thiefSpawn = (GameObject)Instantiate (theifOBJ, spawnRange, theifOBJ.transform.rotation);
				thiefSpawn.tag = "P1Theif";
			}


		} else if (EventManager.playerTurnToken == 2) {
			Vector3 spawnRange = new Vector3(23, 3.2f,(Random.Range (-22,-36)));
			Vector3 hitPoint = new Vector3 (spawnRange.x, 6, spawnRange.z);
			RaycastHit hit;
			if (Physics.Raycast(hitPoint, -Vector3.up, out hit, 2.8f)){
				if (hit.collider.tag == "P2Theif"){
					print ("Already There");
				} 
			}else {
				thiefSpawn = (GameObject)Instantiate (theifOBJ, spawnRange, theifOBJ.transform.rotation);
				thiefSpawn.tag = "P2Theif";
			}
		}
	}
}










