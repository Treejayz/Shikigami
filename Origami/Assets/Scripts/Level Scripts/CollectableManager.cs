﻿using System.Collections;
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

	public static bool[] storybookpages = new bool[7];
	public static bool[] spellbookpages = new bool[5];
	private static bool started;

	public static void Startup(){
		if (!started) {
			for (int i = 0; i < 7; i++) {
				storybookpages [i] = false;
			} 
			for (int i = 0; i < 5; i++) {// find number of pages later
				spellbookpages [i] = false;
			} 
			CollectableManager.storybookpages [1] = true;
			started = true;
		}

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
				}
				else if (SceneManager.GetActiveScene().name == "Level 2"){
					level2paperPieces += 1;
				}
				else { //if (SceneManager.GetActiveScene().name == "Level 3"){
					level3paperPieces += 1;
				}
                break;
		case Collectable.CollectableType.SCRAP:
				GameObject.Find ("ScrapDisplay").GetComponent<ScrapDisplay> ().collect (Time.time);
                scrapPieces += 1;
                MonoBehaviour.print(scrapPieces);
				if (SceneManager.GetActiveScene().name == "Level 1"){
					level1scrapPieces += 1;
				}
				else if (SceneManager.GetActiveScene().name == "Level 2"){
					level2scrapPieces += 1;
				}
				else {//if (SceneManager.GetActiveScene().name == "Level 3"){
					level3scrapPieces += 1;
				}
                break;
        };     
    }
}
