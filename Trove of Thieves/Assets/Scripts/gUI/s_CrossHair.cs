using UnityEngine;
using System.Collections;

public class s_CrossHair : MonoBehaviour {

	RaycastHit mouseCollider;
	public Camera P1Camera, P2Camera;
	public GameObject top, bot;
	public GameObject crossHorTop, crossVerTop;
	public GameObject crossHorBot, crossVerBot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = P1Camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out mouseCollider, 1000)) {
			if (mouseCollider.collider.tag == "P1Ground") {
				top.SetActive (true);
				Screen.showCursor = false;
				Transform trans = crossHorTop.GetComponent<Transform>();
				trans.position = new Vector3 (trans.position.x, trans.position.y, (int)mouseCollider.point.z);
				trans = crossVerTop.GetComponent<Transform>();
				trans.position = new Vector3 ((int)mouseCollider.point.x, trans.position.y, trans.position.z);
			} else { 
				top.SetActive (false); 
				Screen.showCursor = true;
			}
		}else {
			top.SetActive (false);
			Screen.showCursor = true;
		}

		ray = P2Camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out mouseCollider, 1000)) {
			if (mouseCollider.collider.tag == "P2Ground") {
				bot.SetActive (true);
				Screen.showCursor = false;
				Transform trans = crossHorBot.GetComponent<Transform>();
				trans.position = new Vector3 (trans.position.x, trans.position.y, mouseCollider.point.z);
				trans = crossVerBot.GetComponent<Transform>();
				trans.position = new Vector3 (mouseCollider.point.x, trans.position.y, trans.position.z);
			} else { 
				bot.SetActive (false); 
				Screen.showCursor = true;
			}
		} else { 
			bot.SetActive (false);
		}

	}
}
