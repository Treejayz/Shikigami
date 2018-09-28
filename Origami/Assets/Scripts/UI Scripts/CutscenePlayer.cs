using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour {
    GameObject cam;
    bool playing;

    public GameObject FUCKINGGARBAGEAGAHAHGAGA;

	public GameObject other;
    float speed;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera");
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing)
        {
            if (Input.anyKey)
            {
                if (speed < 4f)
                {
                    speed += Time.deltaTime * 6f;
                }
                else
                {
                    speed = 4f;
                }

            }
            else
            {
                if (speed > 1f)
                {
                    speed -= Time.deltaTime * 6f;
                }
                else
                {
                    speed = 1f;
                }
            }

            //Time.timeScale = speed;
            cam.GetComponent<UnityEngine.Video.VideoPlayer>().playbackSpeed = speed;
            AkSoundEngine.SetRTPCValue("MusicSpeed", speed);

            if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                AkSoundEngine.StopAll();
                AkSoundEngine.SetRTPCValue("MusicSpeed", 1f);
                LevelLoader.scene = "Level 1";
                SceneManager.LoadScene("LoadLevel");
            }
            if (cam.GetComponent<UnityEngine.Video.VideoPlayer>().time >= cam.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length - .1f)
            {
                AkSoundEngine.StopAll();
                AkSoundEngine.SetRTPCValue("MusicSpeed", 1f);
                LevelLoader.scene = "Level 1";
                SceneManager.LoadScene("LoadLevel");
            }
        }
	}

    public void playCutscene()
    {
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("Cutscene", cam);
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        playing = true;
		other.SetActive (false);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        FUCKINGGARBAGEAGAHAHGAGA.SetActive(true);
    }
}
