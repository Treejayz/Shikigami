using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPillar : MonoBehaviour {

    public GameObject sprite;
    public GameObject trigger;
    public GameObject pillar;
    public float buildTime = 1f;

	// Use this for initialization
	void Start () {
        sprite.SetActive(true);
        trigger.SetActive(false);
        pillar.SetActive(false);
        StartCoroutine("Build");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Build()
    {
        float currentTime = 0f;
        while (currentTime < buildTime)
        {
            float progress = Mathf.Sin((currentTime / buildTime) * Mathf.PI * .5f);
            sprite.transform.localScale = new Vector3(progress, progress, progress);

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        StartCoroutine("Boom");
    }

    IEnumerator Boom()
    {
        trigger.SetActive(true);
        pillar.SetActive(true);
        float currentTime = 0f;
        while (currentTime < 1f)
        {
            if (currentTime < .1f)
            {
                float scale = Mathf.Sin((currentTime / .1f) * Mathf.PI * .5f);
                pillar.transform.localScale = new Vector3(1f, scale, 1f);
            } else if (currentTime > .6f)
            {
                float scale = Mathf.Sin(((1f - currentTime) / .4f) * Mathf.PI * .5f);
                pillar.transform.localScale = new Vector3(1f, scale, 1f);
            }

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }

}
