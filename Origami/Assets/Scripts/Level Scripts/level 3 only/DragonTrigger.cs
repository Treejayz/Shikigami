using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTrigger : MonoBehaviour
{
    static int numTriggers;

    public GameObject Dragon;
    public Transform pointA, pointB;

    public DragonMover.MoveType moveType;
    public ShadowFire.FireType fireType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            numTriggers += 1;
            DragonMover mover = Dragon.GetComponent<DragonMover>();
            mover.pointA = pointA;
            mover.pointB = pointB;
            mover.Type = moveType;
            ShadowFire fire = Dragon.GetComponentInChildren<ShadowFire>();
            fire.Type = fireType;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Exit");
            numTriggers -= 1;
            if (numTriggers == 0)
            {
                DragonMover mover = Dragon.GetComponent<DragonMover>();
                mover.Type = DragonMover.MoveType.NONE;
                ShadowFire fire = Dragon.GetComponentInChildren<ShadowFire>();
                fire.Type = ShadowFire.FireType.NONE;
            }
        }
    }
}
