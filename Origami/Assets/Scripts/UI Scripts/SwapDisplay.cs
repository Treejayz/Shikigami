using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapDisplay : MonoBehaviour {
	public Sprite story;
	public Sprite spell;
	public GameObject spellbook;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (spellbook.GetComponent<BookSystem>().isactive ()) {
			this.gameObject.GetComponent<Image> ().sprite = story;
		} else {
			this.gameObject.GetComponent<Image> ().sprite = spell;
		}
	}
}
