using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsStuff : MonoBehaviour {

    public Animator anim;
    float speed;

	// Update is called once per frame
	void Update () {
        
        if (Input.anyKey)
        {
            if (speed < 4f)
            {
                speed += Time.deltaTime * 6f;
            } else
            {
                speed = 4f;
            }
            
        }
        else
        {
            if (speed > 1f)
            {
                speed -= Time.deltaTime * 6f;
            } else
            {
                speed = 1f;
            }
        }

        Time.timeScale = speed;
        //Asmodeus codeus

        if (Input.GetKey(KeyCode.Escape))
        {
            AkSoundEngine.StopAll();
            LevelLoader.scene = "Main Menu";
            SceneManager.LoadScene("LoadLevel");
        }
	}
}
