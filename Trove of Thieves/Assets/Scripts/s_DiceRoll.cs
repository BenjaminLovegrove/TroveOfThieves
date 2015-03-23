using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_DiceRoll : MonoBehaviour {

	public List<int> bagOne, bagTwo;
	public int dieOne, dieTwo;

	// Use this for initialization
	void Start () {
		PopulateBag ();
	}

	#region Dice Rolls
	/* Both these fuction will be called from other scripts,
	 * Returning the value of a dice roll and
	 * internaly handling the possabilities of different rolls
	 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
	 * Checks if bag has items then randomly picks an item, returns the number then deletes it from the bag
	 * if the bag is empty it will re populate before finding a new number */
	public int DoRollOne(){
		if (bagOne.Count < 1)
			PopulateBag();

		int ran = Random.Range (0, bagOne.Count);
		int value = bagOne [ran];
		bagOne.RemoveAt (ran);
		return value;
	}

	public int DoRollTwo(){
		if (bagTwo.Count < 1)
			PopulateBag();
		
		int ran = Random.Range (0, bagTwo.Count);
		int value = bagTwo [ran];
		bagTwo.RemoveAt (ran);
		return value;
	}
	#endregion

	//Re poplulates the bag,
	//Clears bag first for glich protection
	void PopulateBag(){
		bagOne.Clear ();
		bagTwo.Clear ();
		for (int i = 1; i <= 6; i++) {
			bagOne.Add (i);
			bagTwo.Add (i);
		}
	}
}
