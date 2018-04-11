using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject cam;
    public GameObject player;

	// Use this for initialization
	void Start () {
        player.GetComponent<Character>().SetState(new StartState(player.GetComponent<Character>()));
        
        //Destroy(this.gameObject);
	}
}
