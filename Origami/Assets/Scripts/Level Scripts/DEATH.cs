using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEATH : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Character>().SetState(new DeathState(other.GetComponent<Character>()));
        }
    }
}
