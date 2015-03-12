using UnityEngine;
using System.Collections;

public class s_TurnDestroy : MonoBehaviour {

	public int destroyAfterTurns;
	int turnCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (turnCount >= destroyAfterTurns)
			Destroy (this.gameObject);
	}

	void TurnCheck(){
		turnCount ++;
	}
}
