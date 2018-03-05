using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tightrope : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Character.sneaking == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        } else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
	}
}
