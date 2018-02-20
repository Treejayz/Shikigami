using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour {

	public bool activated = false;
    private bool hasBeen = false;
    public Transform pos;

	public static GameObject[] CheckPointList;

    public static Text checkPointText;

	// Use this for initialization
	void Start () {
		CheckPointList = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (pos == null)
        {
            pos = this.transform;
        }
        checkPointText = GameObject.Find("CheckPointText").GetComponent<Text>();
        checkPointText.color = new Color(checkPointText.color.r, checkPointText.color.g, checkPointText.color.b, 0f);
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
        if (!hasBeen)
        {
            // We deactive all checkpoints in the scene
            foreach (GameObject cp in CheckPointList)
            {
                cp.GetComponent<Checkpoint>().activated = false;
            }

            // We activate the current checkpoint
            activated = true;
            hasBeen = true;
            StartCoroutine("DisplayText");
        }
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

    private IEnumerator DisplayText()
    {
        print("triggered");
        checkPointText.color = new Color(checkPointText.color.r, checkPointText.color.g, checkPointText.color.b, 1f);
        yield return new WaitForSeconds(3f);
        while (checkPointText.color.a > 0f)
        {
            float newAlpha = checkPointText.color.a - Time.fixedDeltaTime;
            if (newAlpha < 0f) { newAlpha = 0f; }
            checkPointText.color = new Color(checkPointText.color.r, checkPointText.color.g, checkPointText.color.b, newAlpha);
            yield return new WaitForFixedUpdate();
        }
    }
}
