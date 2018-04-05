using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTrigger : MonoBehaviour {

    public float totalTime;

    public GameObject platforms;
    public Material platMat;
    Color platMatColor;


    bool triggered;

    private void Start()
    {
        triggered = false;
        platMat = new Material(platforms.GetComponentInChildren<MeshRenderer>().material);
        platMatColor = platMat.color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine("Triggered");
                triggered = true;
            }
            else
            {
                StopCoroutine("Triggered");
                StartCoroutine("Triggered");
            }
        }
    }

    IEnumerator Triggered()
    {
        float currentTime = 0f;
        platforms.SetActive(true);
        while (currentTime < totalTime)
        {
            if (currentTime < totalTime/10f)
            {
                float progress = currentTime / (totalTime / 10f);
                ParticleSystem[] plats = platforms.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particles in plats)
                {
                    var newEmission = particles.emission;
                    newEmission.rateOverTimeMultiplier = 100 * progress;
                }
                platMatColor.a = progress * .1f;
                platMat.color = platMatColor;
                MeshRenderer[] meshes = platforms.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.material = platMat;
                }
            } else if (currentTime < totalTime * .8f)
            {
                float progress = ((.8f * totalTime) - currentTime) / (totalTime * .8f);
                ParticleSystem[] plats = platforms.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particles in plats)
                {
                    var newEmission = particles.emission;
                    newEmission.rateOverTimeMultiplier = 100 * progress;
                    //particles.emission = newEmission;
                }
                platMatColor.a = progress * .1f;
                platMat.color = platMatColor;
                MeshRenderer[] meshes = platforms.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.material = platMat;
                }
            }

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        platforms.SetActive(false);
        triggered = false;
    }

}
