using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstScrapCheck : MonoBehaviour {
	
	string displayText;
	GameObject displayCanvas;
	float displaytime;
	float fadeTime;

	// Use this for initialization
	void Start () {
		displayText = "Collects extra pieces of the spellbook to gain bonus knowledge about the two spirits!";
		displayCanvas = GameObject.Find("TutorialDisplay");
		displaytime = 3.0f;
		fadeTime = 1.5f;
	}
	
	// Update is called once per frame
	public void Display () {
		StartCoroutine("DisplayText");
	}

	private IEnumerator DisplayText(){
		Color tempcoltext = displayCanvas.GetComponentInChildren<Text>().color;
		Color tempcolimage = displayCanvas.GetComponentInChildren<Image>().color;
		while (displayCanvas.GetComponentInChildren<Text>().color.a > 0f) {
			yield return new WaitForFixedUpdate();
		}
		displayCanvas.GetComponentInChildren<Text> ().text = displayText;
		while (tempcoltext.a < 1f){
			float newAlpha = displayCanvas.GetComponentInChildren<Text>().color.a + (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha > 1f) { newAlpha = 1f; }
			tempcoltext = displayCanvas.GetComponentInChildren<Text>().color;
			tempcoltext.a = newAlpha;
			tempcolimage = displayCanvas.GetComponentInChildren<Image>().color;
			tempcolimage.a = newAlpha;
			displayCanvas.GetComponentInChildren<Image>().color = tempcolimage;
			displayCanvas.GetComponentInChildren<Text> ().color = tempcoltext;
			yield return new WaitForFixedUpdate();
		}
		yield return new WaitForSeconds(displaytime);
		while (tempcoltext.a > 0f){
			float newAlpha = displayCanvas.GetComponentInChildren<Text>().color.a - (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha < 0f) { newAlpha = 0f; }
			tempcoltext = displayCanvas.GetComponentInChildren<Text>().color;
			tempcoltext.a = newAlpha;
			tempcolimage = displayCanvas.GetComponentInChildren<Image>().color;
			tempcolimage.a = newAlpha;
			displayCanvas.GetComponentInChildren<Image>().color = tempcolimage;
			displayCanvas.GetComponentInChildren<Text> ().color = tempcoltext;
			yield return new WaitForFixedUpdate();
		}
	}
}

