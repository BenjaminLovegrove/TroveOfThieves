using UnityEngine;
using System.Collections;

public class s_ButtonUI : MonoBehaviour {

	s_EventManager EventManager;
	bool rolledDice = false;

	public Texture atk1, atk2, atk3, atk4, atk5, atk6;
	public Texture def1, def2, def3, def4, def5, def6;
	public GameObject atkOBJ1, atkOBJ2;
	public GameObject defOBJ1, defOBJ2;
	Vector3 objSpawn = new Vector3(100, 100, 100);

	// Use this for initialization
	void Start () {
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(EventManager.playerTurnToken != 3)
			SpellsButtonGUI ();
		if (!rolledDice && EventManager.playerTurnToken != 3) {
			if (GUI.Button (new Rect (10, Screen.height / 2 - 37.5f, 75, 75), "Roll Dice")) {
				rolledDice = true;
				EventManager.RollDicePressed ();
			}
		}
		if (EventManager.playerTurnToken != 3) {
			if (GUI.Button (new Rect (Screen.width - 85, Screen.height / 2 - 37.5f, 75, 75), "End Turn")) {
				EventManager.EndTurnPressed ();
				rolledDice = false;
			}
		}
	}

	void SpellsButtonGUI(){
		//Def Spells
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 6, Screen.height / 2 - 37.5f, 75, 75), atk1)) {
			Instantiate (atkOBJ1, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 5, Screen.height / 2 - 37.5f, 75, 75), atk2)) {
			Instantiate (atkOBJ2, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 4, Screen.height / 2 - 37.5f, 75, 75), "ATK 3")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 3, Screen.height / 2 - 37.5f, 75, 75), "ATK 4")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85 * 2, Screen.height / 2 - 37.5f, 75, 75), "ATK 5")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 85, Screen.height / 2 - 37.5f, 75, 75), "ATK 6")) {
			
		}
		
		//Atk Spells
		if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.height / 2 - 37.5f, 75, 75), def1)) {
			Instantiate (defOBJ1, objSpawn, defOBJ1.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 1 + 10, Screen.height / 2 - 37.5f, 75, 75), def2)) {
			Instantiate (defOBJ2, objSpawn, defOBJ2.transform.rotation);
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 2 + 10, Screen.height / 2 - 37.5f, 75, 75), "DEF 3")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 3 + 10, Screen.height / 2 - 37.5f, 75, 75), "DEF 4")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 4 + 10, Screen.height / 2 - 37.5f, 75, 75), "DEF 5")) {
			
		}
		if (GUI.Button (new Rect (Screen.width / 2 + 85 * 5 + 10, Screen.height / 2 - 37.5f, 75, 75), "DEF 6")) {
			
		}
		
	}
}
