using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {

    public static int paperPieces;

	// Use this for initialization
	void Start () {
        paperPieces = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Collect()
    {
        paperPieces += 1;
        print(paperPieces);
    }
}
