using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillDragoon : MonoBehaviour {

    public GameObject dragon;
    public GameObject player;
    public GameObject cam;

    public GameObject canvas;

    public Transform particles;
    public Transform target;

    public GameObject fadeOut;

    public AnimationCurve anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Die");
            //LevelLoader.scene = "Credits";
            //SceneManager.LoadScene("LoadLevel");
            //AkSoundEngine.StopAll();
        }

        
    }

    IEnumerator Die()
    {
        //Turn stuff off
        player.GetComponent<forwardtest>().enabled = false;
        player.GetComponent<Character>().enabled = false;
        cam.GetComponent<CameraController>().enabled = false;
        dragon.GetComponent<LookAtPlayer>().enabled = false;
        dragon.GetComponent<DragonMover>().enabled = false;
        dragon.GetComponentInChildren<ShadowFire>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        canvas.SetActive(false);
        AkSoundEngine.StopAll();

        // Position things
        dragon.transform.position = new Vector3(-400f, 85f, 130f);
        dragon.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        cam.transform.position = new Vector3(-400f, 95f, 110f);
        cam.transform.LookAt(dragon.transform.position - Vector3.up * 2f);
        player.transform.position = new Vector3(-410f, 74.5f, 130f);
        player.transform.rotation = Quaternion.Euler(0f, 135f, 0f);
        player.GetComponent<Character>().craneAnimator.SetBool("Moving", false);
        player.GetComponent<Character>().craneAnimator.SetBool("Jumping", false);
        player.GetComponent<Character>().craneAnimator.SetBool("Falling", false);
        player.GetComponent<Character>().craneAnimator.Play("Crane_Idle");
        player.GetComponent<Character>().frogAnimator.SetBool("Moving", false);
        player.GetComponent<Character>().frogAnimator.SetBool("Jumping", false);
        player.GetComponent<Character>().frogAnimator.Play("Frog_Idle");
        player.GetComponent<Character>().foxAnimator.SetBool("Moving", false);
        player.GetComponent<Character>().foxAnimator.SetBool("Jumping", false);
        player.GetComponent<Character>().foxAnimator.SetBool("Sneaking", false);
        player.GetComponent<Character>().foxAnimator.SetBool("Sprinting", false);
        player.GetComponent<Character>().foxAnimator.Play("Fox_Idle");

        // Do things over time
        dragon.GetComponentInChildren<Animator>().Play("Dragon_Roar", 0, 0f);
        AkSoundEngine.PostEvent("DragoonDeath", dragon.gameObject);
        float currentTime = 0f;
        float t = 0f;
        while (currentTime < 3f)
        {
            dragon.GetComponentInChildren<Animator>().speed = (1f - currentTime/3f);

            float yPos = 85f + anim.Evaluate(currentTime/3f) * 27f;

            dragon.transform.position = new Vector3(-400f, yPos, 130f);

            cam.transform.LookAt(dragon.transform.position - Vector3.up * 2f);
            yield return new WaitForFixedUpdate();
            currentTime += Time.fixedDeltaTime;
        }
        yield return new WaitForSeconds(.5f);
        dragon.transform.GetChild(0).gameObject.SetActive(false);
        dragon.transform.GetChild(2).gameObject.SetActive(false);
        dragon.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
        dragon.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);

        // And now the player
        cam.transform.position = new Vector3(-410f, 78f, 122f);
        cam.transform.LookAt(player.transform.position);

        yield return new WaitForSeconds(2f);
        particles.transform.position = player.transform.position;
        ParticleSystem[] parts = particles.GetComponentsInChildren<ParticleSystem>();
        parts[0].Play();
        parts[1].Play();
        parts[2].Play();
        player.SetActive(false);

        yield return new WaitForSeconds(3f);
        cam.transform.position = new Vector3(-370f, 110f, 155f);
        cam.transform.LookAt(particles.position);
        float maxdistane = Vector3.Distance(particles.transform.position, target.position);
        float speed = 0;
        while(Vector3.Distance(particles.transform.position, target.position) > 3f)
        {
            speed += 40 * Time.fixedDeltaTime;
            if (speed > 50)
            {
                speed = 50f;
            }
            particles.position = Vector3.MoveTowards(particles.position, target.position, speed * Time.fixedDeltaTime);
            cam.transform.LookAt(particles.position);
            float curdis = Mathf.Clamp01(1f - ((float)Vector3.Distance(particles.transform.position, target.position) / maxdistane) + .05f);
            fadeOut.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, curdis);
            yield return new WaitForFixedUpdate();
        }

        // Aaaaand scene
        LevelLoader.scene = "Credits";
        SceneManager.LoadScene("LoadLevel");
        AkSoundEngine.StopAll();
    }
}
