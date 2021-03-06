using UnityEngine;
using System.Collections;

public class s_ObjectPlacement : MonoBehaviour {
	
	public int itemCost;

	s_EventManager EventManager;
	public AudioClip noGoldAudio;
	public AudioClip invalidAudio;
	public AudioClip objPlaceAudio;

	Vector3 altObjectRot = new Vector3 (0, 180, 0);

	Renderer fadeoutP1, fadeoutP2;
	public Material fadeoutNull, fadeoutFaded;
	float fadeSpeed = 7.5f;

	// Used For moving and placing OBJ
	RaycastHit mouseCollider;
	bool positionSet = false;
	public Material setMaterial;

	//Used for detectiong P1 or P2 ground
	public Camera P1Camera, P2Camera;
	Camera detectionCamera;

	//IF doent match up to player turn, then next turn must have been pressed. So delete object if not set
	public int destroyTrigger;

	// Use this for initialization
	void Start () {
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
		P1Camera = (Camera)GameObject.Find ("P1 Camera").GetComponent<Camera>();
		P2Camera = (Camera)GameObject.Find ("P2 Camera").GetComponent<Camera>();
		fadeoutP1 = (Renderer)GameObject.Find ("FadeoutP1").GetComponent<Renderer>();
		fadeoutP2 = (Renderer)GameObject.Find ("FadeoutP2").GetComponent<Renderer>();
		if (EventManager.playerTurnToken == 1) {
			destroyTrigger = 1;
			detectionCamera = P1Camera;
		} else if (EventManager.playerTurnToken == 2) {
			transform.eulerAngles = altObjectRot;
			destroyTrigger = 2;
			detectionCamera = P2Camera;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!positionSet) {
			if (EventManager.playerTurnToken == 1){
				fadeoutP1.material.color = Vector4.Lerp (fadeoutP1.material.color, fadeoutNull.color, fadeSpeed * Time.deltaTime);
				fadeoutP2.material.color = Vector4.Lerp (fadeoutP2.material.color, fadeoutFaded.color, fadeSpeed * Time.deltaTime);
			} else {
				fadeoutP1.material.color = Vector4.Lerp (fadeoutP1.material.color, fadeoutFaded.color, fadeSpeed * Time.deltaTime);
				fadeoutP2.material.color = Vector4.Lerp (fadeoutP2.material.color, fadeoutNull.color, fadeSpeed * Time.deltaTime);
			}
			SetDestroyCheck();
			Ray ray = detectionCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out mouseCollider, 1000)) {
				if (mouseCollider.collider.tag == "P1Ground" && destroyTrigger == 1) {
					MoveObjectToMouse();
				}
				if (mouseCollider.collider.tag == "P2Ground" && destroyTrigger == 2) {
					MoveObjectToMouse();
				}
			} else if (Input.GetMouseButtonDown (0)){
				fadeoutP1.material = fadeoutNull;
				fadeoutP2.material = fadeoutNull;
				Destroy (this.gameObject);
			}
		}
	}

	void MoveObjectToMouse(){
		Vector3 newPos = new Vector3 ((int)mouseCollider.point.x, 3.2f, (int)mouseCollider.point.z);
		transform.position = newPos;
		if (Input.GetMouseButtonDown (0)){
			if (mouseCollider.collider.tag == "P1Ground" && destroyTrigger == 1) {
				if (EventManager.playerOneAP < itemCost){
					Camera.main.audio.clip = noGoldAudio;
					Camera.main.audio.Play ();
				} else {
					EventManager.playerOneAP -= itemCost;
					PlaceObject();
				}
			}
			if (mouseCollider.collider.tag == "P2Ground" && destroyTrigger == 2) {
				if (EventManager.playerTwoAP < itemCost){
					Camera.main.audio.clip = noGoldAudio;
					Camera.main.audio.Play ();
				} else {
					EventManager.playerTwoAP -= itemCost;
					PlaceObject();
				}
			} 
		}
		//Allow player to cancel placement
		if (Input.GetMouseButtonDown (1)) {
			Destroy (this.gameObject);
			fadeoutP1.material = fadeoutNull;
			fadeoutP2.material = fadeoutNull;
		}
	}

	void PlaceObject(){
		Camera.main.audio.clip = objPlaceAudio;
		Camera.main.audio.Play ();
		gameObject.renderer.material = setMaterial;
		positionSet = true;
		gameObject.collider.enabled = true;
		fadeoutP1.material = fadeoutNull;
		fadeoutP2.material = fadeoutNull;
	}

	void SetDestroyCheck(){
		if (EventManager.playerTurnToken == 1 && destroyTrigger != 1) {
			Destroy (this.gameObject);
		}
		if (EventManager.playerTurnToken == 2 && destroyTrigger != 2){
			Destroy (this.gameObject);
		}
	}
}
