using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEATH : MonoBehaviour {


    void OnCollisionEnter(Collision other)
    {
        print("hi");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().SetState(new DeathState(other.gameObject.GetComponent<Character>()));
        }
    }
}
