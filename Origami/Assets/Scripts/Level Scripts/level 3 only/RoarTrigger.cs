using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarTrigger : MonoBehaviour {

    public GameObject player;
    public GameObject dragon;
    public GameObject cam;
    public GameObject canvas;


    public static bool triggered = false;
    Vector3 camPosition;
    Quaternion camRotation;

    private void Start()
    {
        if (triggered == true)
        {
            triggered = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !triggered)
        {
            player.GetComponent<forwardtest>().enabled = false;
            player.GetComponent<Character>().enabled = false;
            cam.GetComponent<CameraController>().enabled = false;
            canvas.SetActive(false);
            camPosition = cam.transform.position;
            camRotation = cam.transform.rotation;

            triggered = true;
            //magic numbers 
            cam.transform.position = dragon.transform.position + Vector3.up * 1 + dragon.transform.forward * -9 + dragon.transform.right * 3;
            cam.transform.LookAt(dragon.transform.position + dragon.transform.forward * 5 + Vector3.down * 4f);
            StartCoroutine("Rawr");
        }
    }

    IEnumerator Rawr()
    {
        Animator dragonAnimator = dragon.GetComponentInChildren<Animator>();
        dragonAnimator.Play("Dragon_Idle", 0, 0.7f);
        dragonAnimator.SetTrigger("Ror");

        while (dragonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dragon_Idle"))
        {
            yield return new WaitForEndOfFrame();
        }
        print("rawr");
        yield return new WaitForSeconds(0.5f);
        Vector3 camPos = cam.transform.position;
        float time = 0;
        while (time < 1f)
        {
            cam.transform.position = camPos + Random.insideUnitSphere * ((1 - time) * .5f);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        cam.transform.position = camPos;
        while (dragonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dragon_Roar"))
        {
            yield return new WaitForEndOfFrame();
        }
        print("ok im done");
        yield return new WaitForSeconds(0.5f);
        cam.transform.position = camPosition;
        cam.transform.rotation = camRotation;
        player.GetComponent<forwardtest>().enabled = true;
        player.GetComponent<Character>().enabled = true;
        dragon.GetComponent<LookAtPlayer>().enabled = true;
        dragon.GetComponent<DragonMover>().enabled = true;
        dragon.GetComponentInChildren<ShadowFire>().enabled = true;
        cam.GetComponent<CameraController>().enabled = true;
        canvas.SetActive(true);
        AkSoundEngine.PostEvent("BossBattleStart", cam);
    }
}
