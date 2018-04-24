using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public GameObject eventSystem;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (!this.GetComponent<Canvas> ().enabled) {
			//eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
		}
	}
}