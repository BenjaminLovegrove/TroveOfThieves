using UnityEngine;
using System.Collections;

public class s_StopDestroy : MonoBehaviour {
	int random;
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	// Just tempoary for backup build
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit ();
		}
		if (Application.loadedLevel == 2) {
			Destroy (this.gameObject);
		}
	}
}
