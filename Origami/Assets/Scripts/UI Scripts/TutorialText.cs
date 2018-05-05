using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {
	public GameObject displayCanvas;
	public string message;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			displayCanvas.GetComponent<TutorialDisplay> ().AddToQueue (message);
			Destroy (this.gameObject);
		}
	}
}
