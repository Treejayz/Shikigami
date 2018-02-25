using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardtest : MonoBehaviour {

    public Transform camTrans;
    public Transform camTargetTrans;

    private bool moving;
    [HideInInspector]
    public static bool wall;
    [HideInInspector]
    public static Vector3 forward;
    [HideInInspector]
    public static bool respawn;

    // Use this for initialization
    void Start () {
        moving = false;
        wall = false;
        respawn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 camDir = camTargetTrans.position - camTrans.position;
        camDir = new Vector3(camDir.x, 0.0f, camDir.z);
        camDir.Normalize();
        if (respawn)
        {
            this.transform.rotation = Quaternion.LookRotation(camDir);
            respawn = false;
        }
        camDir = camDir * Input.GetAxis("Vertical") + Quaternion.Euler(0f, 90f, 0f) * camDir * Input.GetAxis("Horizontal");
        camDir.Normalize();
        forward = camDir;
        if (wall)
        {
            // Do nuthin
        }
        else if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.Slerp(this.transform.forward, camDir, 0.05f));
        } else if (moving)
        {
            moving = false;
        }
	}
    
}
