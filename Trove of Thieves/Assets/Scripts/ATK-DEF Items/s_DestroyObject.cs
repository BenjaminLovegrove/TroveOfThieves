using UnityEngine;
using System.Collections;

public class s_DestroyObject : MonoBehaviour {

	public int itemCost;
	public string targetObject;
	
	s_EventManager EventManager;
	public AudioClip noGoldAudio;
	public AudioClip invalidAudio;

	// Used For moving and destroying OBJ
	RaycastHit mouseCollider;
	RaycastHit leftClick;
	public Material setMaterial;
	
	//Used for detectiong P1 or P2 ground
	public Camera P1Camera, P2Camera;
	Camera detectionCamera;

	//IF doent match up to player turn, then next turn must have been pressed. So delete object if not set
	public int destroyTrigger;


	// Use this for initialization
	void Start () {
		//Finds Event manager and Maps Cameras
		EventManager = (s_EventManager)GameObject.Find ("EventManager").GetComponent <s_EventManager> ();
		P1Camera = (Camera)GameObject.Find ("P1 Camera").GetComponent<Camera>();
		P2Camera = (Camera)GameObject.Find ("P2 Camera").GetComponent<Camera>();
		//Sets trigger against oppposite players camera
		if (EventManager.playerTurnToken == 1) {
			destroyTrigger = 1;
			detectionCamera = P2Camera;
		} else if (EventManager.playerTurnToken == 2) {
			destroyTrigger = 2;
			detectionCamera = P1Camera;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ClickObject ();
		// Sets position to oppistes player camera only.
		// Casts a ray and sets pos only if e.g. its P1 turn and ray hits P2 ground
		Ray ray = detectionCamera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out mouseCollider, 1000)) {
			if (mouseCollider.collider.tag == "P1Ground" && destroyTrigger == 2) {
				Vector3 newPos = new Vector3 ((int)mouseCollider.point.x, 3.2f, (int)mouseCollider.point.z);
				transform.position = newPos;
			}
			if (mouseCollider.collider.tag == "P2Ground" && destroyTrigger == 1) {
				Vector3 newPos = new Vector3 ((int)mouseCollider.point.x, 3.2f, (int)mouseCollider.point.z);
				transform.position = newPos;
			}
		}
	}

	void ClickObject(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray2 = detectionCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray2, out leftClick, 1000)) {
				// Checks collision tag against prefabs hit variable,
				// If == then checks whos turn it is and actions accordingly
				if (leftClick.collider.tag == targetObject){
					if (EventManager.playerTurnToken == 1) {
						if (EventManager.playerOneGold < itemCost){
							Camera.main.audio.clip = noGoldAudio;
							Camera.main.audio.Play ();
						} else {
							EventManager.playerOneGold -= itemCost;
							Destroy (leftClick.collider.gameObject);
							Destroy (this.gameObject);
						}
					}
					if (EventManager.playerTurnToken == 2) {
						if (EventManager.playerTwoGold < itemCost){
							Camera.main.audio.clip = noGoldAudio;
							Camera.main.audio.Play ();
						} else {
							EventManager.playerTwoGold -= itemCost;
							Destroy (leftClick.collider.gameObject);
							Destroy (this.gameObject);
						}
					}
				}
				// Destroys if not clicked against hit variable and plays sound.
				else {
					Camera.main.audio.clip = invalidAudio;
					Camera.main.audio.Play ();
					Destroy (this.gameObject);
				}
			} 
			// Destroys if not clicked against hit variable
			// This is repeted in case there is NO collision with ray
			else {
				Destroy (this.gameObject);
			}
		}
		//Allow player to cancel placement
		if (Input.GetMouseButtonDown (1)) {
			Destroy (this.gameObject);
		}
	}

}
