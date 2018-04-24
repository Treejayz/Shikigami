using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setMenuSelect(GameObject button){
		GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (button);
	}
	public void setMenuSelectNull(){
		GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
	}

	public void loadScene(string scenename){
        AkSoundEngine.StopAll();
        LevelLoader.scene = scenename;
		SceneManager.LoadScene ("LoadLevel");
	}
	public void loadCanvas(GameObject canvasName){
		this.GetComponent<Canvas> ().enabled = false;
		canvasName.GetComponent<Canvas> ().enabled = true;
        // AkSoundEngine.PostEvent("HPMusic", gameObject);
    }
	public void unPause(){
		this.GetComponent<Canvas> ().enabled = false;
        // AkSoundEngine.PostEvent("ResetHPMusic", gameObject);
    }
	public void quit(){
		Application.Quit();
	}
	public void restart(GameObject player){
		player.GetComponent<Character> ().SetState (new DeathState(player.GetComponent<Character>()));
		this.GetComponent<Canvas> ().enabled = false;
	}
	public void reset(){
		CollectableManager.Reset ();
		RoarTrigger.triggered = false;
	}
}
