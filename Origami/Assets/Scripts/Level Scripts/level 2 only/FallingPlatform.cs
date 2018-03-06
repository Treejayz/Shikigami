using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    float shakeTime = .4f;
    float fallTime = 1f;
    float shakeAmount = 0.2f;
    float fallAcceleration = 30f;

    float respawntime = .5f;
    public AnimationCurve popIn;

    public Transform plat;
    private Vector3 platStartPos;
    private Vector3 platStartScale;
    public Transform platVisual;
    private Vector3 platVisualStart;

    private bool triggered;

    void Start () {
        triggered = false;
        platStartPos = plat.position;
        platVisualStart = platVisual.position;
        platStartScale = plat.localScale;
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
            platVisual.position = new Vector3(platVisualStart.x + x, platVisualStart.y, platVisualStart.z + z);
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("Fall");
    }

    IEnumerator Fall()
    {
        float currentSpeed = 0f;
        float currentTime = 0f;
        while (currentTime < fallTime)
        {
            currentSpeed += fallAcceleration * Time.fixedDeltaTime;
            plat.transform.position += Vector3.down * currentSpeed * Time.fixedDeltaTime;
            plat.transform.localScale = platStartScale * ((fallTime - currentTime) / fallTime);

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        plat.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        plat.gameObject.SetActive(true);
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
