using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updraft : MonoBehaviour {

    
    public float upForce;
    float currentSpeed;
    GameObject player;

    bool floating;
    bool inTrigger;

	// Use this for initialization
	void Start () {
        currentSpeed = 0f;
        floating = false;
        inTrigger = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (floating)
        {
            if (player.GetComponent<CharacterController>().isGrounded)
            {
                floating = false;
                currentSpeed = 0f;
            }
            else
            {
                player.GetComponent<CharacterController>().Move(new Vector3(0f, currentSpeed * Time.fixedDeltaTime, 0f));
                if (!inTrigger)
                {
                    currentSpeed -= upForce * Time.fixedDeltaTime;
                    if (currentSpeed <= 0f)
                    {
                        floating = false;
                    }
                }
            }
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            floating = true;
            inTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hi");
            if (currentSpeed < player.GetComponent<Character>().gravity && !player.GetComponent<CharacterController>().isGrounded)
            {
                floating = true;
                currentSpeed += upForce * Time.fixedDeltaTime;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
