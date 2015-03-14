using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_TooltipUI : MonoBehaviour {

	public Image tooltipImage;

	public void Enter(){
		tooltipImage.gameObject.SetActive (true);
	}
	public void Exit(){
		tooltipImage.gameObject.SetActive (false);
	}

}
