using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_MainMenu : MonoBehaviour {

	public Image tuteImage, creditsImage;

	public void ButtonPress(string buttonString){
		if (buttonString == "Play")
			tuteImage.gameObject.SetActive (true);
		if (buttonString == "Continue")
			Application.LoadLevel (1);
		if (buttonString == "Credits")
			creditsImage.gameObject.SetActive (true);
		if (buttonString == "Close Credits")
			creditsImage.gameObject.SetActive (false);
	}
}
