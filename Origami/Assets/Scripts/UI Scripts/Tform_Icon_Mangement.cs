using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tform_Icon_Mangement : MonoBehaviour {
	
	public GameObject pause;
	public GameObject player;
	Component[] images;
	// Use this for initialization
	void Start () {
		images = GetComponentsInChildren<Image>();
	}

	// Update is called once per frame
	void Update () {
		if (pause.GetComponent<PauseSystem> ().pause) {
			this.GetComponent<Canvas> ().enabled = false;
		} else {
			this.GetComponent<Canvas> ().enabled = true;
		}
		if (!player.GetComponent<Character>().canFrog) {
			this.GetComponent<Canvas> ().enabled = false;
		}
		else if (player.GetComponent<Character>().canFrog && !player.GetComponent<Character> ().canFox) {
			if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FROG) {
				foreach (Image panel in images){
					if (panel.gameObject.name == "E_Crane" || panel.gameObject.name == "Q_Crane") {
						panel.enabled = true;
					} else {
						panel.enabled = false;
					}
				}
			}  else {// crane form
				foreach (Image panel in images){
					if (panel.gameObject.name == "E_Frog" || panel.gameObject.name == "Q_Frog") {
						panel.enabled = true;
					} else {
						panel.enabled = false;
					}
				}
			}

		}
		else { /// has fox and frog page
			if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FROG) {
				foreach (Image panel in images){
					if (panel.gameObject.name == "E_Fox" || panel.gameObject.name == "Q_Crane") {
						panel.enabled = true;
					} else {
						panel.enabled = false;
					}
				}
			} else if (player.GetComponent<Character> ().GetForm() == Character.CurrentForm.FOX) {
				foreach (Image panel in images){
					if (panel.gameObject.name == "E_Crane" || panel.gameObject.name == "Q_Frog") {
						panel.enabled = true;
					} else {
						panel.enabled = false;
					}
				}
			} else { //form crane
				foreach (Image panel in images){
					if (panel.gameObject.name == "E_Frog" || panel.gameObject.name == "Q_Fox") {
						panel.enabled = true;
					} else {
						panel.enabled = false;
					}
				}
			}
		}	
	}
}
