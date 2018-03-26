using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhilePause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponentInParent<PauseSystem> ().pause == true) {
			this.enabled = true;
		} else {
			this.enabled = false;
		}
	}
}
