using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILL : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("hi");
        if (other.tag == "Player")
        {
            other.GetComponent<Character>().SetState(new DeathState(other.GetComponent<Character>()));
            other.GetComponent<Character>().dead = true;
        }
    }
}
