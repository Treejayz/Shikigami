using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tform_Icon_Mangement : MonoBehaviour {
	
	public GameObject pause;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (pause.GetComponent<Canvas> ().enabled) {
			this.GetComponent<Canvas> ().enabled = false;
		} else {
			this.GetComponent<Canvas> ().enabled = true;
		}
		if (!player.GetComponent<Character>().canFrog) {
			this.GetComponent<Canvas> ().enabled = false;
		}
		/*if (player.GetComponent<Character> ().canFox) {
			if (player.GetComponent<Character> ().GetForm () == "Frog") {
				// show crane in q and fox in e
			} else if (player.GetComponent<Character> ().GetForm () == "Fox") {
				//show crane in q and frog in e
			} else {
				//show frog in q and fox in e
			}
		} else {
			if (player.GetComponent<Character> ().GetForm () == "Frog") {
				//code to show crane in both q and e
			}  else {
				//code to show crane in both q and e
			}
		} */
	}
}
