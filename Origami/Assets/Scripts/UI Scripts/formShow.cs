using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class formShow : MonoBehaviour {

	public GameObject player;
	public string type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (type == "Frog") {
			if (player.GetComponent<Character> ().GetForm () == Character.CurrentForm.FROG) {
				this.gameObject.GetComponent<Image> ().enabled = false;
			} else {
				this.gameObject.GetComponent<Image> ().enabled = true;
			}
		}
		if (type == "Crane") {
			if (player.GetComponent<Character> ().GetForm () == Character.CurrentForm.FROG) {
				this.gameObject.GetComponent<Image> ().enabled = true;
			} else {
				this.gameObject.GetComponent<Image> ().enabled = false;
			}
		}
	}
}
