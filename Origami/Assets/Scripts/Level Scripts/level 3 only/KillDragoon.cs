using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDragoon : MonoBehaviour {

    public GameObject dragon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelLoader.scene = "Credits";
            SceneManager.LoadScene("LoadLevel");
        }

        AkSoundEngine.StopAll();
    }
}
