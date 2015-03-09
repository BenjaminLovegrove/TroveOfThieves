using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_MainMenu : MonoBehaviour {

	public void ButtonPress(string buttonString){
		if (buttonString == "Play")
			Application.LoadLevel (1);
	}
}
