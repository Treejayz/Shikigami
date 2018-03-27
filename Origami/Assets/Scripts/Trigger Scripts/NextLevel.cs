using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public int level = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AkSoundEngine.StopAll();
            if (level == 1)
            {
                SceneManager.LoadScene("Level 2");
            }
            else if (level == 2)
            {
                SceneManager.LoadScene("Level 3");
            }
        }
    }
}
