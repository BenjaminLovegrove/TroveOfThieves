using UnityEngine;
using System.Collections;

public class s_StopDestroy : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	// Just tempoary for backup build
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit ();
		}
	}
}
