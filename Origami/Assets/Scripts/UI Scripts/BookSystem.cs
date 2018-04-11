                                           using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookSystem : MonoBehaviour {
	int activepage1 = 0;
	int activepage2 = 1;
	public GameObject[] pages;
	bool[] pageshave;// change this to grab from collectables CollectableManager.storybookpages CollectableManager.spellbookpages
	public GameObject prev;
	public GameObject next;
	public Sprite missing;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < pages.Length; i++){
			if (!pageshave [i]) {
				pages [i].GetComponent<Image> ().sprite = missing;
			}
			if (i <= activepage1) {
				//set to the left side of page and at an increasing order
			} else {
				//set to the right side of page and at an increasing order
			}
		}
		if (this.enabled){
			if (activepage1 <= 0) {
				prev.gameObject.GetComponent<Button> ().enabled = false;
				prev.gameObject.GetComponent<Image> ().enabled = false;
			} else {
				prev.gameObject.GetComponent<Button> ().enabled = true;
				prev.gameObject.GetComponent<Image> ().enabled = true;
			}
			if (activepage1 >= pages.Length - 1) {
				next.gameObject.GetComponent<Button> ().enabled = false;
				next.gameObject.GetComponent<Image> ().enabled = false;
			} else {
				next.gameObject.GetComponent<Button> ().enabled = true;
				next.gameObject.GetComponent<Image> ().enabled = true;
			}
		}
	}

	public void next_page(){
		if (activepage2 < pages.Length) {
			activepage1++;
			activepage2++;
		}
	}
	public void prev_page(){
		if (activepage1 > 0) {
			activepage1--;
			activepage2--;
		}
	}
	public void activate(){
		activepage1=0;
		activepage2=1;
		foreach (Image display in this.GetComponentsInChildren<Image>()){
			display.enabled = true; 
		}
	}
	public void deactivate(){
		activepage1=0;
		activepage2=1;
		foreach (Image display in this.GetComponentsInChildren<Image>()){
			display.enabled = false; 
		}
	}
}
