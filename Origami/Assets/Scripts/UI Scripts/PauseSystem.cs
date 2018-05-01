using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {
	public bool pause = false; 
	bool cantpause;
	// Use this for initialization
	void Start () {
		CollectableManager.Startup ();
		bool cantpause = true;
	}
	
	// Update is called once per frame
	void Update () {
		//MonoBehaviour.print (GameObject.Find ("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ().currentSelectedGameObject);
		Component[] canvases;
		canvases = GetComponentsInChildren<Canvas>();
		pause = false; 
		foreach (Canvas can in canvases){
			if (can.enabled && can.name != "Fade") {
				pause = true;
			}
		}
		if (!cantpause && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))) {
			if (!pause) {
				this.transform.Find ("Pause Menu").GetComponent<Canvas> ().enabled = true;
				GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (GameObject.Find ("Continue"));
			} else {
				foreach (Canvas can in canvases) {
					GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
					can.enabled = false;
				}
			}
		}
		if (pause) {
			Cursor.visible = true;
			Time.timeScale = 0.0F;
			AkSoundEngine.SetRTPCValue("MenuUp", 1.0f, null);
		} else {
			Time.timeScale = 1.0F;
			Cursor.visible = false;
			AkSoundEngine.SetRTPCValue("MenuUp", 0.0f, null);
		}
		if (pause) {
			this.transform.Find ("Fade").GetComponent<Canvas> ().enabled = true;
		} else {
			this.transform.Find ("Fade").GetComponent<Canvas> ().enabled = false;
		}
	}
	public void cantpauseon(){
		cantpause = true;
	}
	public void cantpauseoff(){
		cantpause = false;
	}
}
