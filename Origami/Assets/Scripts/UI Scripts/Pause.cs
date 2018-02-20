using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public GameObject eventSystem;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			this.GetComponent<Canvas> ().enabled = !this.GetComponent<Canvas> ().enabled;
		}
		if (this.GetComponent<Canvas> ().enabled) {
			Time.timeScale = 0.0F;
		} else {
			Time.timeScale = 1.0F;
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
		}
	}
}