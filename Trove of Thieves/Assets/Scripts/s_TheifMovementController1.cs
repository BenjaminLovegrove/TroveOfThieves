using UnityEngine;
using System.Collections;

public class s_TheifMovementController1 : MonoBehaviour {

	Vector3 newPos;
	bool canMove = true;

	// Use this for initialization
	void Start () {
		newPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		CollisionDetection ();
		if (canMove)
			transform.position = Vector3.Lerp (transform.position, newPos, 1 * Time.deltaTime);
	}
	
	void DoMove(int steps){
		newPos = transform.position + -Vector3.left * steps;
	}
	
	void CollisionDetection(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.forward, out hit, 1)) {
			if (hit.collider.tag == "Block"){
				canMove = false;
			} else {
				canMove = true;
			}
		}
	}
}
