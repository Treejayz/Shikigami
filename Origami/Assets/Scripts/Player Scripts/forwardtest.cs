using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardtest : MonoBehaviour {

    public Transform camTrans;
    public Transform camTargetTrans;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 camDir = camTargetTrans.position- camTrans.position;
        camDir = new Vector3(camDir.x, 0.0f, camDir.z);
        camDir = camDir / camDir.magnitude;
        this.transform.rotation = Quaternion.LookRotation(Vector3.Slerp(this.transform.forward, camDir, 0.1f));
	}
}
