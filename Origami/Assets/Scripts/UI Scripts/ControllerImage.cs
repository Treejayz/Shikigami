using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerImage : MonoBehaviour {
	public Sprite control;
	public Sprite keyboard;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetJoystickNames ().GetLength(0) == 0) {
			this.gameObject.GetComponent<Image> ().sprite = keyboard;
		} else {
			this.gameObject.GetComponent<Image> ().sprite = control;
		}
	}
}
