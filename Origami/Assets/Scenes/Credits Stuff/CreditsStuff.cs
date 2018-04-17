using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsStuff : MonoBehaviour {

    public Animator anim;

	// Update is called once per frame
	void Update () {
        /*
        if (Input.anyKey)
        {
            print("hi");
            //anim.SetFloat("Speed", 2f);
            Time.timeScale = 3f;
        }
        else
        {
            anim.SetFloat("Speed", 1f);
            Time.timeScale = 1f;
        }
        */
        if (Input.GetKey(KeyCode.Escape))
        {
            AkSoundEngine.StopAll();
            LevelLoader.scene = "Main Menu";
            SceneManager.LoadScene("LoadLevel");
        }
	}
}
