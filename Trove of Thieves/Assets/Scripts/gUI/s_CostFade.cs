using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_CostFade : MonoBehaviour {

	public s_EventManager EventManager;
	public Button thisButton;

	public int cost;

	void Start(){
		thisButton = GetComponent<Button>();
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
	}

	// Update is called once per frame
	void Update () {
	
		if (EventManager.playerTurnToken == 1) {
			if (EventManager.playerOneAP > cost){
				thisButton.interactable = true;
			} else {
				thisButton.interactable = false;
			}
		}

		if (EventManager.playerTurnToken == 2) {
			if (EventManager.playerTwoAP > cost){
				thisButton.interactable = true;
			} else {
				thisButton.interactable = false;
			}
		}

	}
}
