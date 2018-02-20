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
	}
}
