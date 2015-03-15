using UnityEngine;
using System.Collections;

public class s_StopDestroy : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
}
