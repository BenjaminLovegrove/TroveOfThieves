using UnityEngine;
using System.Collections;

public class s_TheifAnimation : MonoBehaviour {

	public Animation anim;
	s_TheifMovementController MovementController;

	bool isMoving = false;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animation> ();
		MovementController = GetComponent<s_TheifMovementController> ();
		anim["run"].speed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		isMoving = MovementController.canMove;
		if (isMoving) {
			anim.CrossFade ("run");
		} else {
			anim.CrossFade ("idle");
		}
	}
}
