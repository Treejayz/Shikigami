using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour {
	public string displayText;
	public GameObject displayCanvas;
	float fadeTime;

	// Use this for initialization
	void Start () {
		fadeTime= 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			displayCanvas.GetComponentInChildren<Text> ().text = displayText;
			StartCoroutine("DisplayText");
			new WaitForSeconds(7.0f);
		}
	}

	private IEnumerator DisplayText()
	{
		Color tempcoltext = displayCanvas.GetComponentInChildren<Text>().color;
		Color tempcolimage = displayCanvas.GetComponentInChildren<Image>().color;
		while (tempcoltext.a < 1f)
		{
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
		yield return new WaitForSeconds(3.0f);
		while (tempcoltext.a > 0f)
		{
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
		Destroy (this.gameObject);
	}
}
