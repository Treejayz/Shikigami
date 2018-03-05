using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public Transform plat;
    private Transform platStart;
    public Transform platVisual;
    public Transform platVisualStart;

    private bool triggered;

    void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !Character.sneaking && !triggered)
        {
            triggered = true;
            StartCoroutine("Shake");
        }
    }

    IEnumerator Shake()
    {
        yield return null;
    }
}
