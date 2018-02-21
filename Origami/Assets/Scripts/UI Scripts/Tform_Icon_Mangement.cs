using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tform_Icon_Mangement : MonoBehaviour {
	
	public GameObject pause;
	public GameObject player;
	Component[] renderers;
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
		if (player.GetComponent<Character> ().canFox) {
			if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FROG) {
				
			} else if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FOX) {
				
			} else {
				
			}
		} else {
			if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FROG) {

			}  else {
				
			}
		} 	
	}
}
