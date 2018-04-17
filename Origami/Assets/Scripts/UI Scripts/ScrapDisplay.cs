 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrapDisplay : MonoBehaviour {

	int temp;
	int leveltotal;
	public float fadeTime;
	public float timer;
	float timeoflast;
	bool going;
	public bool piece;

	// Use this for initialization
	void Start () {
		timeoflast = -10f;
		bool going = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!piece) {
			if (SceneManager.GetActiveScene ().name == "Level 1") {
				temp = CollectableManager.level1scrapPieces;
				leveltotal = 100;
			} else if (SceneManager.GetActiveScene ().name == "Level 2") {
				temp = CollectableManager.level2scrapPieces;
				leveltotal = 130;
			} else {//if (SceneManager.GetActiveScene().name == "Level 3"){
				temp = CollectableManager.level3scrapPieces;
				leveltotal = 0;
			}
		} else {
			if (SceneManager.GetActiveScene ().name == "Level 1") {
				temp = CollectableManager.level1paperPieces;
				leveltotal = 8;
			} else if (SceneManager.GetActiveScene ().name == "Level 2") {
				temp = CollectableManager.level2paperPieces;
				leveltotal = 9;
			} else {//if (SceneManager.GetActiveScene().name == "Level 3"){
				temp = CollectableManager.level3paperPieces;
				leveltotal = 0;
			}
		}
		if (temp == 0) {
			this.GetComponent<Image> ().enabled = false;
			GetComponentInChildren<Text> ().enabled = false;
		} else {
			this.GetComponent<Image> ().enabled = true;
			GetComponentInChildren<Text> ().enabled = true;
		}
		if (timeoflast + timer > Time.time) {
			StopCoroutine ("loweroppacity");
			going = false;
			Color tempcoltext = this.GetComponentInChildren<Text> ().color;
			Color tempcolimage = this.GetComponent<Image> ().color;
			tempcoltext = this.GetComponentInChildren<Text> ().color;
			tempcoltext.a = 1f;
			tempcolimage = this.GetComponent<Image> ().color;
			tempcolimage.a = 1f;
			this.GetComponent<Image> ().color = tempcolimage;
			this.GetComponentInChildren<Text> ().color = tempcoltext;
		} else if (!going && temp !=0) {
			StartCoroutine ("loweroppacity");
		}
		GetComponentInChildren<Text> ().text = temp.ToString() + "/" + leveltotal.ToString();
	}
	public void collect(float timereset){
		timeoflast = timereset;
	}
	private IEnumerator loweroppacity()
	{
		going = true;
		Color tempcoltext = this.GetComponentInChildren<Text>().color;
		Color tempcolimage = this.GetComponent<Image>().color;
		while (tempcoltext.a > 0f)
		{
			float newAlpha = this.GetComponentInChildren<Text>().color.a - (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha < 0f) { newAlpha = 0f; }
			tempcoltext = this.GetComponentInChildren<Text>().color;
			tempcoltext.a = newAlpha;
			tempcolimage = this.GetComponent<Image>().color;
			tempcolimage.a = newAlpha;
			this.GetComponent<Image>().color = tempcolimage;
			this.GetComponentInChildren<Text>().color = tempcoltext;
			yield return new WaitForFixedUpdate();
		}
		going = false;
	}
}
