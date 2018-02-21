﻿using System.Collections;
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

	public void loadScene(string scenename){
		SceneManager.LoadScene (scenename);
	}
	public void loadCanvas(GameObject canvasName){
		this.GetComponent<Canvas> ().enabled = false;
		canvasName.GetComponent<Canvas> ().enabled = true;
	}
	public void unPause(){
		this.GetComponent<Canvas> ().enabled = false;
	}
	public void quit(){
		Application.Quit();
	}
	public void restart(GameObject player){
		player.GetComponent<Character> ().SetState (new DeathState(player.GetComponent<Character>()));
		this.GetComponent<Canvas> ().enabled = false;
	}
}