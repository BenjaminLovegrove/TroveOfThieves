﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_ButtonUI : MonoBehaviour {

	s_EventManager EventManager;
	bool rolledDice = false;
	bool canEnd = false;

	public AudioClip noGoldAudio;

	public Button rollDiceButton, endTurnButton;

	public GameObject atkOBJ1, atkOBJ2;
	public GameObject defOBJ1, defOBJ2, defOBJ3, defOBJ4;
	Vector3 objSpawn = new Vector3(100, 100, 100);
	public Texture UIBackdrop;

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
		if (!rolledDice) {
			rollDiceButton.interactable = true;
			rollText.text = "Roll";
		} else { 
			rollDiceButton.interactable = false; 
			rollText.text = ". . .";
		}
		if (canEnd){
			endTurnButton.interactable = true;
			endText.text = "End";
		} else {
			endTurnButton.interactable = false;
			endText.text = ". . .";
		}

	}

	public void EndButtonTimed(bool condition){
		Text endText = endTurnButton.GetComponentInChildren<Text>();
		Text rollText = rollDiceButton.GetComponentInChildren<Text>();
		if (condition) {
			endTurnButton.interactable = true;
			endText.text = "End";
			rollDiceButton.interactable = true;
			rollText.text = "Roll";
			rolledDice = false;
		}
		if (!condition) {
			endTurnButton.interactable = false;
			endText.text = ". . .";
			rollDiceButton.interactable = false; 
			rollText.text = ". . .";
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
			canEnd = true;
			EventManager.RollDicePressed ();
		}
		if (buttonName == "EndTurn") {
			canEnd = false;
			EventManager.EndTurnPressed ();
			//rolledDice = true;
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
		if (buttonName == "ATK4")
			StealGoldSpell ();

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

	void StealGoldSpell(){
		if (EventManager.playerTurnToken == 1) {
			EventManager.playerOneAP -= 8;
			EventManager.playerOneGold += 100;
			EventManager.playerTwoGold -= 100;
		} else {
			EventManager.playerTwoAP -= 8;
			EventManager.playerOneGold -= 100;
			EventManager.playerTwoGold += 100;
		}
	}

	void SpawnThiefs(){
		if (EventManager.playerTurnToken == 1) {
			if (EventManager.playerOneAP >= 2){
				Vector3 spawnRange = new Vector3(37, 3.2f,(Random.Range (-23,-34)));
				Vector3 hitPoint = new Vector3 (spawnRange.x, 6, spawnRange.z);
				RaycastHit hit;
				if (Physics.Raycast(hitPoint, -Vector3.up, out hit, 2.8f)){
					if (hit.collider.tag == "P2Theif"){
						print ("Already There");
					} 
				}else {
					thiefSpawn = (GameObject)Instantiate (theifOBJ, spawnRange, theifOBJ.transform.rotation);
					thiefSpawn.tag = "P1Theif";
					EventManager.playerOneAP -= 2;
				}
			}
		} else if (EventManager.playerTurnToken == 2) {
			if (EventManager.playerTwoAP >= 2){
				Vector3 spawnRange = new Vector3(23, 3.2f,(Random.Range (-23,-34)));
				Vector3 hitPoint = new Vector3 (spawnRange.x, 6, spawnRange.z);
				RaycastHit hit;
				if (Physics.Raycast(hitPoint, -Vector3.up, out hit, 2.8f)){
					if (hit.collider.tag == "P2Theif"){
						print ("Already There");
					} 
				}else {
					thiefSpawn = (GameObject)Instantiate (theifOBJ, spawnRange, theifOBJ.transform.rotation);
					thiefSpawn.tag = "P2Theif";
					EventManager.playerTwoAP -= 2;
				}
			}
		}
	}
}










