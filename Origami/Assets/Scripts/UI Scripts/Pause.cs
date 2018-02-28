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
			Cursor.visible = true;
            Time.timeScale = 0.0F;

            AkSoundEngine.SetRTPCValue("MenuUp", 1.0f, null);
		} else {
			Time.timeScale = 1.0F;
			Cursor.visible = false;
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);

            AkSoundEngine.SetRTPCValue("MenuUp", 0.0f, null);
		}
	}
}