using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTrigger : MonoBehaviour
{

    public GameObject Dragon;
    public Transform pointA, pointB;

    public DragonMover.MoveType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DragonMover mover = Dragon.GetComponent<DragonMover>();
            mover.pointA = pointA;
            mover.pointB = pointB;
            mover.Type = type;
        }
    }
}
