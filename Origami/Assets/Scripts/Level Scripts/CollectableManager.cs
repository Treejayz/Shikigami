using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CollectableManager {

    public static int paperPieces;
    public static int scrapPieces;

	public static int level1paperPieces;
	public static int level1scrapPieces;
	public static int level2paperPieces;
	public static int level2scrapPieces;
	public static int level3paperPieces;
	public static int level3scrapPieces;
	public static int storypage4scraps;
	public static int storypage4scrapmax;
	public static int storypage5scraps;
	public static int storypage5scrapmax;
	public static int storypage6scraps;
	public static int storypage6scrapmax;
	public static int storypage7scraps;
	public static int storypage7scrapmax;
	public static int spellpage1pieces;
	public static int spellpage1piecemax;
	public static int spellpage2pieces;
	public static int spellpage2piecemax;
	public static int spellpage3pieces;
	public static int spellpage3piecemax;
	public static int spellpage4pieces;
	public static int spellpage4piecemax;

	public static bool[] storybookpages = new bool[7];
	public static bool[] spellbookpages = new bool[4];
	private static bool started;

	public static void Startup(){
		if (!started) {
			for (int i = 0; i < 7; i++) {
				storybookpages [i] = false;
			} 
			for (int i = 0; i < 4; i++) {// find number of pages later
				spellbookpages [i] = false;
			} 
			CollectableManager.storybookpages [1] = true;
			started = true;
		}
		storypage4scraps = 0;
		storypage4scrapmax = 80;
		storypage5scraps = 0;
		storypage5scrapmax = 110;
		storypage6scraps = 0;
		storypage6scrapmax = 60;
		storypage7scraps = 0;
		storypage7scrapmax = 50;

		spellpage1pieces = 0;
		spellpage1piecemax = 10;
		spellpage2pieces = 0;
		spellpage2piecemax = 10;
		spellpage3pieces = 0;
		spellpage3piecemax = 6;
		spellpage4pieces = 0;
		spellpage4piecemax = 6;
	}

    public static void Reset()
    {
        paperPieces = 0;
		scrapPieces = 0;
		level1paperPieces = 0;
		level1scrapPieces = 0;
		level2paperPieces = 0;
		level2scrapPieces = 0;
		level3paperPieces = 0;
		level3scrapPieces = 0;	
    }

    public static void Collect(Collectable.CollectableType type)
    {
        switch(type)
        {
				case Collectable.CollectableType.PIECE:
				GameObject.Find ("PieceDisplay").GetComponent<ScrapDisplay> ().collect (Time.time);
                paperPieces += 1;
                MonoBehaviour.print(paperPieces);
				if (SceneManager.GetActiveScene().name == "Level 1"){
					level1paperPieces += 1;
					spellpage1pieces++;
				}
				else if (SceneManager.GetActiveScene().name == "Level 2"){
					level2paperPieces += 1;
					if (level2paperPieces > 2) {
						spellpage2pieces++;
					} else {
						spellpage1pieces++;
					}
				}
				else { //if (SceneManager.GetActiveScene().name == "Level 3"){
					level3paperPieces += 1;
					spellpage2pieces++;
				}
                break;
		case Collectable.CollectableType.SCRAP:
				GameObject.Find ("ScrapDisplay").GetComponent<ScrapDisplay> ().collect (Time.time);
                scrapPieces += 1;
                MonoBehaviour.print(scrapPieces);
				if (SceneManager.GetActiveScene().name == "Level 1"){
					level1scrapPieces += 1;
					if (storypage4scraps < storypage4scrapmax) { 
						storypage4scraps++;
					} else {
						storypage7scraps++;
					}
					if (storypage4scraps == storypage4scrapmax) { 
						CollectableManager.storybookpages [3] = true;
					} 
				}
				else if (SceneManager.GetActiveScene().name == "Level 2"){
					level2scrapPieces += 1;
					if (storypage5scraps < storypage5scrapmax) { 
						storypage5scraps++;
					} else {
						storypage7scraps++;
					}
					if (storypage5scraps == storypage5scrapmax) { 
						CollectableManager.storybookpages [4] = true;
					} 
				}
				else {//if (SceneManager.GetActiveScene().name == "Level 3"){
					level3scrapPieces += 1;
					if (storypage6scraps < storypage6scrapmax) { 
						storypage6scraps++;
					} else {
						storypage7scraps++;
					}
					if (storypage6scraps == storypage6scrapmax) { 
						CollectableManager.storybookpages [5] = true;
					} 
					if (storypage7scraps == storypage7scrapmax) { 
						CollectableManager.storybookpages [6] = true;
					} 
				}
                break;
        };     
    }
}
