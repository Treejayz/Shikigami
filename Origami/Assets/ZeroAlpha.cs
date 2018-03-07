using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeroAlpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color tempcol = this.gameObject.GetComponent<Image> ().color;
		tempcol.a = 0.0f;
		this.gameObject.GetComponent<Image> ().color = tempcol;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
