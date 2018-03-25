using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public Transform player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = transform.position - player.position;
        direction.y = 0f;
        direction.Normalize();
        direction = Vector3.Lerp(transform.forward, direction, 3 * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(direction);
	}
}
