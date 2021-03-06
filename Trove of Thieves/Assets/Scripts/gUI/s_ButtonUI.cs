﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_ButtonUI : MonoBehaviour {

	s_EventManager EventManager;

	public AudioClip noGoldAudio;
	public AudioClip coins;

	public Button endTurnButton;
	public float endTurnButtonCD = 0;

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
		//UnityGuiController ();
		endTurnButtonCD -= Time.deltaTime;
		if (endTurnButtonCD < 0) {
			endTurnButton.interactable = true;
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
		if (buttonName == "EndTurn") {
			EventManager.EndTurnPressed ();
			endTurnButton.interactable = false;
			endTurnButtonCD = 2f;
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
		Camera.main.audio.clip = coins;
		Camera.main.audio.Play ();
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










