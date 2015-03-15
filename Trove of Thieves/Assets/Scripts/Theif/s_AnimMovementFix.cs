using UnityEngine;
using System.Collections;

public class s_AnimMovementFix : MonoBehaviour {

	float setY;

	// Use this for initialization
	void Start () {
		setY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (this.transform.position.x, setY, this.transform.position.z);
	}
}
