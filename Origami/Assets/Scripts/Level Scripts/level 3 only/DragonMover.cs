using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMover : MonoBehaviour {

    public float speed = 40f;
    private float easeDistance = 3f;
    public Transform player;

    public Transform pointA;
    public Transform pointB;

    public Vector3 centerPos = new Vector3(-400f, 0f, 0f);

    private Vector3 startPos;
    private Vector3 newPos;
    private float currentSpeed;

    public enum MoveType { PARALLEL, LEAD, CHASE, NONE };
    public MoveType Type;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
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
            case MoveType.NONE:
                Vector3 targetDir = player.transform.position - centerPos;
                targetDir.y = 0f;
                targetDir.Normalize();
                newPos = player.transform.position + targetDir * -30f + Vector3.up * 15f;
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
