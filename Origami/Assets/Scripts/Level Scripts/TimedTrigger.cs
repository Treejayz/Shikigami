using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTrigger : MonoBehaviour {

    public float totalTime;

    public GameObject platforms;
    public Material platMat;
    Color platMatColor;

    public Transform botSand;
    public Transform topSand;
    public ParticleSystem sandDropper;
    public ParticleSystem poof;


    bool triggered;

    private void Start()
    {
        triggered = false;
        platMat = new Material(platforms.GetComponentInChildren<MeshRenderer>().material);
        platMatColor = platMat.color;
        botSand.localScale = new Vector3(1f, 1f, 1f);
        topSand.localScale = new Vector3(0f, 0f, 0f);
        sandDropper.Stop();
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
        sandDropper.Play();
        poof.Play();
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
            } else if (currentTime < totalTime * .9f)
            {
                float progress = ((.9f * totalTime) - currentTime) / (totalTime * .9f);
                ParticleSystem[] plats = platforms.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particles in plats)
                {
                    var newEmission = particles.emission;
                    newEmission.rateOverTimeMultiplier = 90 * progress + 10;
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


            //Sand Scale Stuff
            botSand.localScale = currentTime / totalTime * new Vector3(1f, 1f, 1f);
            topSand.localScale = (1 - (currentTime / totalTime)) * new Vector3(1f, 1f, 1f);

            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        ParticleSystem[] lastPlats = platforms.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particles in lastPlats)
        {
            var newEmission = particles.emission;
            newEmission.rateOverTimeMultiplier = 0;
            //particles.emission = newEmission;
        }
        yield return new WaitForSeconds(.65f);
        platforms.SetActive(false);
        sandDropper.Stop();
        triggered = false;
    }

}
