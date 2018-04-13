using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDragoon : MonoBehaviour {

    public GameObject dragon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(dragon);
            Destroy(this.gameObject);
        }

        AkSoundEngine.StopAll();
    }
}
