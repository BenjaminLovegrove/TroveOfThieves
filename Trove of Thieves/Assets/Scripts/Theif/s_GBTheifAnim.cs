using UnityEngine;
using System.Collections;

public class s_GBTheifAnim : MonoBehaviour {

	public Animator anim;
	s_TheifMovementController MovementController;

	
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		MovementController = GetComponent<s_TheifMovementController> ();
	}

	void Update () {
	
		if (MovementController.canMove == true && anim.GetBool("isMoving") == false) {
			anim.SetBool ("isMoving", true);
		}

		if (!MovementController.canMove && anim.GetBool ("isMoving") == true) {
			anim.SetBool ("isMoving", false);
		}

		if (MovementController.dead == true && anim.GetBool ("dead") == false) {
			anim.SetBool ("dead", true);
		}

	}
}
