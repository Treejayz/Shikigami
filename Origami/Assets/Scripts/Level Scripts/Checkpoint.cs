using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public bool activated = false;
    public Transform pos;

	public static GameObject[] CheckPointList;

	// Use this for initialization
	void Start () {
		CheckPointList = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (pos == null)
        {
            pos = this.transform;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }

    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointList)
        {
            cp.GetComponent<Checkpoint>().activated = false;
        }

        // We activate the current checkpoint
        activated = true;
    }

    public static Vector3 GetPoint()
    {
        Vector3 result = new Vector3(0f, 0f, 0f);

        if (CheckPointList != null)
        {
            foreach (GameObject point in CheckPointList)
            {
                if (point.GetComponent<Checkpoint>().activated)
                {
                    result = point.GetComponent<Checkpoint>().pos.position;
                }
            }
        }
        return result;
    }
}
