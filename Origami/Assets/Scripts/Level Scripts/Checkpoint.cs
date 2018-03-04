﻿using System.Collections;
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

    public static Text checkPointText;
    private static float fadeTime = 2f;

    // Use this for initialization
    void Start() {
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
        checkPointText.color = new Color(checkPointText.color.r, checkPointText.color.g, checkPointText.color.b, 1f);
        yield return new WaitForSeconds(3f);
        while (checkPointText.color.a > 0f)
        {
            float newAlpha = checkPointText.color.a - (Time.fixedDeltaTime * (1f / fadeTime));
            if (newAlpha < 0f) { newAlpha = 0f; }
            checkPointText.color = new Color(checkPointText.color.r, checkPointText.color.g, checkPointText.color.b, newAlpha);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CollectPage()
    {
        float currentspeed = 0f;
        float acceleration = 30f;
        Vector3 startPos = Page.transform.position;
        Vector3 startScale = Page.transform.localScale;
        yield return new WaitForSeconds(0.2f);
        while (Vector3.Distance(Page.transform.position, player.position) > 0.3f)
        {
            if (currentspeed < 40f)
            {
                currentspeed += acceleration * Time.deltaTime;
            }
            Page.transform.Translate((player.position - Page.transform.position).normalized * currentspeed * Time.deltaTime);

            float currentDistance = Vector3.Distance(Page.transform.position, player.position) / Vector3.Distance(startPos, player.position);
            Page.transform.localScale = startScale * currentDistance;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(Page);
    }
}
