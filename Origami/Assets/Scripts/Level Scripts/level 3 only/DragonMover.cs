using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMover : MonoBehaviour {

    public float speed = 25f;
    private float easeDistance = 3f;
    public Transform player;

    public Transform pointA;
    public Transform pointB;

    private Vector3 newPos;
    private float currentSpeed;

    public enum MoveType { PARALLEL, LEAD, CHASE };
    public MoveType Type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = pointB.position - pointA.position;
        Vector3 heading = player.position - pointA.position;
        Vector3 pos = Vector3.Project(heading, direction);


        switch (Type)
        {
            case MoveType.PARALLEL:
                newPos = pointA.position + pos;
                break;

            case MoveType.LEAD:
                newPos = pointA.position + pos + (direction.normalized * 40f);
                break;

            case MoveType.CHASE:
                newPos = pointA.position + pos - (direction.normalized * 40f);
                break;
        };

        if (Vector3.Distance(newPos, transform.position) < 10f)
        {
            currentSpeed = speed * (Vector3.Distance(newPos, transform.position) / 10f);
        } else if (currentSpeed < speed)
        {
            currentSpeed = speed;
        }
	}

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPos, currentSpeed * Time.deltaTime);
    }
}
