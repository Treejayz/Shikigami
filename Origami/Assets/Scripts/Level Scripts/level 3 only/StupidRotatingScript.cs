using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidRotatingScript : MonoBehaviour {

    public float rotateAmount;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, rotateAmount * Time.deltaTime, 0f));
	}
}
