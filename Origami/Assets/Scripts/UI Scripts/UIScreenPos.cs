using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenPos : MonoBehaviour {
	public float percentX;
	public float percentY;
	float x;
	float y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x = Screen.width * percentX;
		y = Screen.height * percentY;
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
