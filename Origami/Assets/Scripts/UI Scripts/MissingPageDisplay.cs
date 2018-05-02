using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissingPageDisplay : MonoBehaviour {
	public bool spell;
	public int pagenum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int pageTotal;
		int pageCurent;
		if (spell) {
			if (pagenum == 1) {
				pageCurent = CollectableManager.spellpage1pieces;
				pageTotal = CollectableManager.spellpage1piecemax;
			} else if (pagenum == 2) {
				pageCurent = CollectableManager.spellpage2pieces;
				pageTotal = CollectableManager.spellpage2piecemax;
			} else if (pagenum == 3) {//if (SceneManager.GetActiveScene().name == "Level 3"){
				pageCurent = CollectableManager.spellpage3pieces;
				pageTotal = CollectableManager.spellpage3piecemax;
			} else {
				pageCurent = CollectableManager.spellpage4pieces;
				pageTotal = CollectableManager.spellpage4piecemax;
			}
		} else {
			if (pagenum == 4) {
				pageCurent = CollectableManager.storypage4scraps;
				pageTotal = CollectableManager.storypage4scrapmax;
			} else if (pagenum == 5) {
				pageCurent = CollectableManager.storypage5scraps;
				pageTotal = CollectableManager.storypage5scrapmax;
			} else if (pagenum == 6) {//if (SceneManager.GetActiveScene().name == "Level 3"){
				pageCurent = CollectableManager.storypage6scraps;
				pageTotal = CollectableManager.storypage6scrapmax;
			} else {
				pageCurent = CollectableManager.storypage7scraps;
				pageTotal = CollectableManager.storypage7scrapmax;
			}
		}
		GetComponentInChildren<Text> ().text = pageCurent.ToString() + " / " + pageTotal.ToString();
	}
}
