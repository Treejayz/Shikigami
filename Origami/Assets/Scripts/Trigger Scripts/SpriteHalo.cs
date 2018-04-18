using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHalo : MonoBehaviour {


    public Transform player;
    public GameObject haloObject;
    public float radius = 5f;
    public int numItems = 8;

    public float spinSpeed = .3f;
    public float objectSpinSpeed = 0f;

    float revealTime = 5f;

    List<GameObject> things = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numItems; i++)
        {
            GameObject thing = Instantiate(haloObject, transform, false);
            thing.transform.localPosition = new Vector3(0f, 0f, 0f);
            print(((float)i / (float)numItems));
            Vector3 direction = (Quaternion.AngleAxis(360f * ((float)i / (float)numItems), Vector3.up) * Vector3.forward).normalized * radius;
            thing.transform.Translate(direction);
            thing.transform.Rotate(transform.up, (360f * ((float)i / (float)numItems)));
            things.Add(thing);
            StartCoroutine("Fade");
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position + Vector3.up;
        transform.Rotate(0f, spinSpeed * Time.deltaTime * 360f, 0f);
        foreach (GameObject thing in things)
        {
            thing.transform.Rotate(0f, objectSpinSpeed * Time.deltaTime * 360f, 0f);
        }

    }

    IEnumerator Fade()
    {
        float currentTime = 0f;
        while (currentTime < 1f)
        {
            Color currentColor = new Color(1f, 1f, 1f, currentTime);
            foreach(GameObject thing in things)
            {
                thing.GetComponent<SpriteRenderer>().color = currentColor;
            }
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(3f);
        currentTime = 0f;
        while (currentTime < 1f)
        {
            Color currentColor = new Color(1f, 1f, 1f, 1f - currentTime);
            foreach (GameObject thing in things)
            {
                thing.GetComponent<SpriteRenderer>().color = currentColor;
            }
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }
}
