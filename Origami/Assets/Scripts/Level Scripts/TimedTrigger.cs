using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTrigger : MonoBehaviour {

    public GameObject platforms;

    bool triggered;

    private void Start()
    {
        triggered = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!triggered)
            {
                platforms.SetActive(true);
                triggered = true;
            }
            else
            {
                platforms.SetActive(false);
                triggered = false;
            }
        }
    }

}
