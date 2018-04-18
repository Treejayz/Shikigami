using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour {
    GameObject cam;
    bool playing;

    public GameObject FUCKINGGARBAGEAGAHAHGAGA;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera");
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing)
        {
            if (cam.GetComponent<UnityEngine.Video.VideoPlayer>().time >= cam.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length - .1f)
            {
                
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

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        FUCKINGGARBAGEAGAHAHGAGA.SetActive(true);
    }
}
