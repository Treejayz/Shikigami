using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDragoon : MonoBehaviour {

    public GameObject dragon;
    public GameObject player;
    public GameObject cam;

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

        // Position things
        dragon.transform.position = new Vector3(-400f, 85f, 130f);
        dragon.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        cam.transform.position = new Vector3(-400f, 95f, 110f);
        cam.transform.LookAt(dragon.transform.position - Vector3.up * 2f);

        // Do things over time
        dragon.GetComponentInChildren<Animator>().Play("Dragon_Roar", 0, 0f);
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
        LevelLoader.scene = "Credits";
        SceneManager.LoadScene("LoadLevel");
        AkSoundEngine.StopAll();
    }
}
