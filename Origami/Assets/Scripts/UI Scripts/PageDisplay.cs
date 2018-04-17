using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageDisplay : MonoBehaviour {
	public Sprite actual;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void page_change (){
		this.GetComponent<Image> ().sprite = actual;
	}
}
