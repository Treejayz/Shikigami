using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCollection : MonoBehaviour {
	public int level;
	int piece;
	int piecetotal;
	int scrap;
	int scraptotal;
	public GameObject Book1;
	public GameObject Book2;
	public GameObject scraptext;
	public GameObject piecetext;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Book1.GetComponentInChildren<BookSystem> ().isactive () || Book2.GetComponentInChildren<BookSystem> ().isactive ()) {
			foreach (Text tex in this.GetComponentsInChildren<Text>()) {
				tex.enabled = false;
			}
			this.GetComponentInChildren<Image> ().enabled = false;
		} else {
			foreach (Text tex in this.GetComponentsInChildren<Text>()) {
				tex.enabled = true;
			}
			this.GetComponentInChildren<Image> ().enabled = true;
		}
		if (level == 1) {
			piece = CollectableManager.level1paperPieces;
			piecetotal = 8;
			scrap = CollectableManager.level1scrapPieces;
			scraptotal = 100;
		} else if (level == 2) {
			piece = CollectableManager.level2paperPieces;
			piecetotal = 9;
			scrap = CollectableManager.level2scrapPieces;
			scraptotal = 130;
		} else {//if (SceneManager.GetActiveScene().name == "Level 3"){
			piece = CollectableManager.level3paperPieces;
			piecetotal = 0;
			scrap = CollectableManager.level3scrapPieces;
			scraptotal = 0;
		}
		scraptext.GetComponentInChildren<Text> ().text = scrap.ToString() + " / " + scraptotal.ToString();
		piecetext.GetComponentInChildren<Text> ().text = piece.ToString() + " / " + piecetotal.ToString();
	}
}
