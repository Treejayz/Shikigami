using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour {

    public bool activated = false;
    private bool hasBeen = false;
    public Transform pos;

    public GameObject Page;
    private Transform player;

    public static GameObject[] CheckPointList;

	public GameObject checkPointText;
    private static float fadeTime = 2f;

    public bool newForm = false;

    // Use this for initialization
    void Start() {
        CheckPointList = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (pos == null)
        {
            pos = this.transform;
        }
        if (activated)
        {
            hasBeen = true;
        }
		checkPointText.GetComponent<Image>().color = new Color(checkPointText.GetComponent<Image>().color.r, checkPointText.GetComponent<Image>().color.g, checkPointText.GetComponent<Image>().color.b, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject.transform;
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
            if (Page != null)
            {
                StartCoroutine("CollectPage");
            }
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

    public static Vector2 GetAngle()
    {
        Vector3 result = new Vector2(0f, 0f);
        if (CheckPointList != null)
        {
            foreach (GameObject point in CheckPointList)
            {
                if (point.GetComponent<Checkpoint>().activated)
                {
                    result.x = point.GetComponent<Checkpoint>().pos.rotation.eulerAngles.x;
                    result.y = point.GetComponent<Checkpoint>().pos.rotation.eulerAngles.y;
                }
            }
        }
        return result;
    }

    private IEnumerator DisplayText()
    {
		Color tempcol = checkPointText.GetComponent<Image>().color;
		while (tempcol.a < 1f)
		{
			float newAlpha = checkPointText.GetComponent<Image>().color.a + (Time.fixedDeltaTime * (1f / fadeTime));
			if (newAlpha > 1f) { newAlpha = 1f; }
			tempcol = checkPointText.GetComponent<Image> ().color;
			tempcol.a = newAlpha;
			checkPointText.GetComponent<Image> ().color = tempcol;
			yield return new WaitForFixedUpdate();
		}
        yield return new WaitForSeconds(.5f);
		while (tempcol.a > 0f)
        {
			float newAlpha = checkPointText.GetComponent<Image>().color.a - (Time.fixedDeltaTime * (1f / fadeTime));
            if (newAlpha < 0f) { newAlpha = 0f; }
			tempcol = checkPointText.GetComponent<Image> ().color;
			tempcol.a = newAlpha;
			checkPointText.GetComponent<Image> ().color = tempcol;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CollectPage()
    {
        if (Page.GetComponent<Page>() != null)
        {
            Page.GetComponent<Page>().enabled = false;
        }
        float currentspeed = 0f;
        float acceleration = 200f;
        Vector3 startPos = Page.transform.position;
        Vector3 startScale = Page.transform.localScale;
        while (Vector3.Distance(Page.transform.position, player.position) > 0.3f)
        {
            if (currentspeed < 150f)
            {
                currentspeed += acceleration * Time.fixedDeltaTime;
            }
            Page.transform.position += ((player.position - Page.transform.position).normalized * currentspeed * Time.fixedDeltaTime);

            float currentDistance = Vector3.Distance(Page.transform.position, player.position) / Vector3.Distance(startPos, player.position);
            Page.transform.localScale = startScale * currentDistance;

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        // I think this is the best place to put this. The player is set as the source because the page should disappear here.
        AkSoundEngine.PostEvent("BigPickup", player.gameObject);
        if (!newForm)
        {
            Destroy(Page);
        }
    }
}
