using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Canvas> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
