using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEATH : MonoBehaviour {

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("hi");
        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<Character>().SetState(new DeathState(hit.gameObject.GetComponent<Character>()));
        }
    }
}
