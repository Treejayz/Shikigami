using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMover : MonoBehaviour {

    public float speed = 25f;
    private float easeDistance = 3f;
    public Transform player;

    public Transform pointA;
    public Transform pointB;

    public enum MoveType { PARALLEL, LEAD, CHASE };
    public MoveType Type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(Type)
        {
            case MoveType.PARALLEL:

                Vector3 direction = pointB.position - pointA.position;
                Vector3 heading = player.position - pointA.position;
                Vector3 pos = Vector3.Project(heading, direction);
                transform.position = pointA.position + pos;
                break;

            case MoveType.LEAD:

                break;

            case MoveType.CHASE:

                break;
        };
	}
}
