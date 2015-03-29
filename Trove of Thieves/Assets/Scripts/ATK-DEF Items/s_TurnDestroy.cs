using UnityEngine;
using System.Collections;

public class s_TurnDestroy : MonoBehaviour {

	public int destroyAfterTurns;
	int turnCount = 0;
	public AudioClip DestroySfx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (turnCount >= destroyAfterTurns)
			Invoke ("Destroy", 5f);
	}

	void TurnCheck(){
		turnCount ++;
	}

	void Destroy(){
		Camera.main.audio.clip = DestroySfx;
		Camera.main.audio.Play ();
		Destroy (this.gameObject);
	}
}
