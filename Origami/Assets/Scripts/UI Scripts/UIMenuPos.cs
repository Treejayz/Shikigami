using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPos : MonoBehaviour {
	public float percentX;
	public float percentY;
	float x;
	float y;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float current_width = (Screen.width-1920)/2;
		float current_height = (Screen.height-1080)/2;
		x = 1920 * percentX + current_width;
		y = 1080 * percentY + current_height;
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}