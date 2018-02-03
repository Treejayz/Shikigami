using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public bool is_Horizontal;
    public float sense;

	
	// Update is called once per frame
	void Update () {

        if (is_Horizontal) {
            float horizontal = Input.GetAxis("Mouse X") * sense * Time.deltaTime;

            transform.Rotate(0, horizontal, 0);
        } else
        {
            float vertical = Input.GetAxis("Mouse Y") * sense * Time.deltaTime;
            transform.Rotate(vertical, 0, 0);
        }
    }
}
