using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrapDisplay : MonoBehaviour {

	int temp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "Level 1"){
			temp = CollectableManager.level1scrapPieces;
		}
		else if (SceneManager.GetActiveScene().name == "Level 2"){
			temp = CollectableManager.level2scrapPieces;
		}
		else {//if (SceneManager.GetActiveScene().name == "Level 3"){
			temp = CollectableManager.level3scrapPieces;
		}
		if (temp == 0) {
			this.GetComponent<Image> ().enabled = false;
			GetComponentInChildren<Text> ().enabled = false;
		} else {
			this.GetComponent<Image> ().enabled = true;
			GetComponentInChildren<Text> ().enabled = true;
		}
		GetComponentInChildren<Text> ().text = temp.ToString();
	}
}
