using UnityEngine;
using System.Collections;

public class s_TheifMovementController : MonoBehaviour {

	s_EventManager EventManager;

	Vector3 newPos;
	Vector3 rayDir = Vector3.left;
	bool canMove = true;
	float moveSpeed;

	// Use this for initialization
	void Start () {
		newPos = transform.position;
		canMove = false;
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		CollisionDetection ();
		/*
		//Stops the theifs from moving outside of turn.
		if (EventManager.playerTurnToken != 3) {
			canMove = false;
		} else { canMove = true; }
		*/
		//Sets the direction and lerps Theif when allowed to move
		if (canMove){
			if (this.gameObject.tag == "P1Theif"){
				transform.position = Vector3.MoveTowards (transform.position, newPos, moveSpeed * Time.deltaTime);
				rayDir = Vector3.right;
			}

			if (this.gameObject.tag == "P2Theif"){
				transform.position = Vector3.MoveTowards (transform.position, newPos, -moveSpeed * Time.deltaTime);
				rayDir = Vector3.left;

			}
		}

	}
	/* This calculates a speed based on the distance needed to cover and the steps
	 * Also sets the position to lerp to and sets the players move ability to true */
	void DoMove(int steps){
		newPos = transform.position + -Vector3.left * steps;
		moveSpeed = steps / (Vector3.Distance (transform.position, newPos));
		canMove = true;
	}
	
	void CollisionDetection(){
		RaycastHit hit;
		// This is to detect objects in front of the thief, here we can make decitions based on object.
		if (Physics.Raycast (transform.position, rayDir, out hit, 0.5f)) {
			if (hit.collider.tag == "Boulder"){
				canMove = false;
			} else if(hit.collider.tag == "Fire"){
				Destroy (this.gameObject);
			}
			else {
				canMove = true;
			}
		}
	}
}
