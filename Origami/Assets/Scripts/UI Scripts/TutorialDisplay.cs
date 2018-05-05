using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour {
	string displayText;
	float displaytime;
	float fadeTime;
	Queue q;
	bool running;


	// Use this for initialization
	void Start () {
		q = new Queue ();
		fadeTime= 1.5f;
		displaytime = 3.0f;
		running = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (q.Count > 0 && !running) {
			running = true;
			displayText = q.Dequeue ().ToString();
			StartCoroutine("DisplayText");
		}
	}

	public void AddToQueue (string text){
		q.Enqueue (text);
	}

	private IEnumerator DisplayText()
	{
		Color tempcoltext = this.GetComponentInChildren<Text>().color;
		Color tempcolimage = this.GetComponentInChildren<Image>().color;
		this.GetComponentInChildren<Text> ().text = displayText;
		while (tempcoltext.a < 1f)
		{
			
			float newAlpha = this.GetComponentInChildren<Text>().color.a + (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha > 1f) { newAlpha = 1f; }
			tempcoltext = this.GetComponentInChildren<Text>().color;
			tempcoltext.a = newAlpha;
			tempcolimage = this.GetComponentInChildren<Image>().color;
			tempcolimage.a = newAlpha;
			this.GetComponentInChildren<Image>().color = tempcolimage;
			this.GetComponentInChildren<Text> ().color = tempcoltext;
			yield return new WaitForFixedUpdate();
		}
		yield return new WaitForSeconds(displaytime);
		while (tempcoltext.a > 0f)
		{
			float newAlpha = this.GetComponentInChildren<Text>().color.a - (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha < 0f) { newAlpha = 0f; }
			tempcoltext = this.GetComponentInChildren<Text>().color;
			tempcoltext.a = newAlpha;
			tempcolimage = this.GetComponentInChildren<Image>().color;
			tempcolimage.a = newAlpha;
			this.GetComponentInChildren<Image>().color = tempcolimage;
			this.GetComponentInChildren<Text> ().color = tempcoltext;
			yield return new WaitForFixedUpdate();
		}
		running = false;
	}
}
