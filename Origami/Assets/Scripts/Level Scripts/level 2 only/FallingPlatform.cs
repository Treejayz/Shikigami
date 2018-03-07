using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    float shakeTime = .4f;
    float fallTime = 1f;
    float shakeAmount = 0.2f;
    float fallAcceleration = 15f;

    float respawntime = .5f;
    public AnimationCurve popIn;

    public GameObject plat;
    private Vector3 platStartPos;
    private Vector3 platStartScale;
    public GameObject platVisual;
    private Vector3 platVisualStart;

    private bool triggered;

    void Start () {
        triggered = false;
        platStartPos = plat.transform.position;
        platVisualStart = platVisual.transform.position;
        platStartScale = plat.transform.localScale;
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
        float currentTime = 0f;
        while (currentTime < shakeTime)
        {
            float currentRatio = (shakeTime - currentTime) / shakeTime;
            float x = Random.Range(currentRatio * -1f, currentRatio) * shakeAmount;
            float z = Random.Range(currentRatio * -1f, currentRatio) * shakeAmount;
            platVisual.transform.position = new Vector3(platVisualStart.x + x, platVisualStart.y, platVisualStart.z + z);
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("Fall");
    }

    IEnumerator Fall()
    {
        float currentSpeed = 5f;
        float currentTime = 0f;
        while (currentTime < fallTime)
        {
            currentSpeed += fallAcceleration * Time.fixedDeltaTime;
            plat.transform.position += Vector3.down * currentSpeed * Time.fixedDeltaTime;
            plat.transform.localScale = platStartScale * ((fallTime - currentTime) / fallTime);

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        plat.GetComponent<MeshCollider>().enabled = false;
        platVisual.GetComponent<MeshRenderer>().enabled = false;
        platVisual.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(3f);
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        plat.GetComponent<MeshCollider>().enabled = true;
        platVisual.GetComponent<MeshRenderer>().enabled = true;
        platVisual.GetComponent<ParticleSystem>().Play();
        plat.transform.position = platStartPos;
        plat.transform.localScale = new Vector3(0f, 0f, 0f);
        float currentTime = 0f;
        while (currentTime < respawntime)
        {
            float currentScale = popIn.Evaluate(currentTime / respawntime);
            plat.transform.localScale = platStartScale * currentScale;
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        triggered = false;
    }
}
