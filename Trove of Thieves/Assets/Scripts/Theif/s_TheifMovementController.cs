﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_TheifMovementController : MonoBehaviour {

	s_EventManager EventManager;
	public AudioClip thiefDeathAudio;
	public AudioClip coinsSFX;
	public AudioClip diceRoll;

	Vector3 newPos;
	Vector3 rayDir = Vector3.left;
	public bool canMove = true;
	public bool touchingIce = false;
	float moveSpeed = 1;
	public bool dead = false;

	public Text moveRoll;

	Vector3 altObjectRot = new Vector3 (0, 180, 0);

	// Use this for initialization
	void Start () {
		newPos = transform.position;
		canMove = false;
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
		if (this.gameObject.tag == "P1Theif") {
			transform.eulerAngles = altObjectRot;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			CollisionDetection ();
			//Sets the direction and lerps Theif when allowed to move
			if (canMove) {
				if (this.gameObject.tag == "P1Theif") {
					transform.position = Vector3.MoveTowards (transform.position, newPos, moveSpeed * Time.deltaTime * 2);
					rayDir = Vector3.right;
				}
			
				if (this.gameObject.tag == "P2Theif") {
					transform.position = Vector3.MoveTowards (transform.position, newPos, moveSpeed * Time.deltaTime * 2);
					rayDir = Vector3.left;
				}
			}
		} else {
			canMove = false;
			newPos = transform.position;
		}
		if (transform.position == newPos){
			canMove = false;
		}
	}
	/* This calculates a speed based on the distance needed to cover and the steps
	 * Also sets the position to lerp to and sets the players move ability to true */
	void DoMove(int steps){
		moveSpeed = 1;
		newPos = transform.position + Vector3.right * steps;
		canMove = true;
		touchingIce = false;
	}
	
	void CollisionDetection(){
		RaycastHit hit;
		// This is to detect objects in front of the thief, here we can make decitions based on object.

		if (Physics.Raycast (transform.position, rayDir, out hit, 0.5f)) {
			if (hit.collider.tag == "Boulder" || hit.collider.tag == "Barricade") {
				canMove = false;
			} else if (hit.collider.tag == "P1Theif" || hit.collider.tag == "P2Theif") {
				canMove = false;
			} else if (hit.collider.tag == "Fire") {
				Camera.main.audio.clip = thiefDeathAudio;
				Camera.main.audio.Play ();
				dead = true;
				Destroy (this.gameObject, 1f);
				Destroy (hit.collider.gameObject, 1f);
			} else if (hit.collider.tag == "IceCube") {
				if (!touchingIce) {
					touchingIce = true;
					float x = Mathf.Abs (transform.position.x) - Mathf.Abs (newPos.x);
					newPos = new Vector3 (newPos.x + x / 2, newPos.y, newPos.z);
					moveSpeed = 0.5f;
				} 
			} else if (hit.collider.tag == "Gold Pile") {
				CollectGold ();
			} else {
				canMove = true;
				touchingIce = false;
			}
		} 
	}

	void CollectGold(){
		Camera.main.audio.clip = coinsSFX;
		Camera.main.audio.Play ();
		if (this.gameObject.tag == "P1Theif") {
			EventManager.playerOneGold += 100;
			EventManager.playerTwoGold -= 100;
			Destroy (gameObject);
		} else {
			EventManager.playerOneGold -= 100;
			EventManager.playerTwoGold += 100;
			Destroy (gameObject);
		}
	}
}
