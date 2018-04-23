using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour {

    public enum Type {LOOP, YOYO, OTHER};
    public Type PlatformType = Type.LOOP;

    private bool Yoyoing = false;

    public Transform[] targets;
    public Transform platform;
    public float speed;
    private float currentSpeed;
    private float currentDistance;
    private float acceleration;

    public bool ease = true;
    public float easeDistance = 3f;

    private int currentTarget;

	// Use this for initialization
	void Start () {
        platform.position = targets[0].position;
        currentTarget = 1;
        currentSpeed = 0f;

        acceleration = ((speed * speed) / (2 * easeDistance));
    }

    private void Update()
    {
        float distance = Vector3.Distance(platform.position, targets[currentTarget].position);

        if (distance < 0.05f || currentSpeed < 0f)
        {
            currentSpeed = 0f;

            switch (PlatformType)
            {
                case Type.LOOP:
                    currentTarget += 1;
                    if (currentTarget == targets.Length)
                    {
                        currentTarget = 0;
                    }
                    break;
                case Type.YOYO:
                    if (Yoyoing)
                    {
                        currentTarget -= 1;
                        if (currentTarget == -1)
                        {
                            currentTarget = 1;
                            Yoyoing = false;
                        }
                    }
                    else
                    {
                        currentTarget += 1;
                        if (currentTarget == targets.Length)
                        {
                            currentTarget -= 2;
                            Yoyoing = true;
                        }
                    }
                    break;
            }
            currentDistance = Vector3.Distance(platform.position, targets[currentTarget].position);
        }
        else if (distance < easeDistance)
        {
            //currentSpeed = speed * Mathf.Sin((distance / easeDistance) * Mathf.PI * 0.5f);
            currentSpeed -= acceleration * Time.deltaTime;
        }
        else if (distance > (currentDistance - easeDistance))
        {
            //currentSpeed = speed * Mathf.Sin((.1f + distance - currentDistance / easeDistance) * Mathf.PI * 0.5f);
            currentSpeed += acceleration * Time.deltaTime;
        } else if (currentSpeed != speed)
        {
            currentSpeed = speed;
        }
    }

    void FixedUpdate () {
        platform.position = Vector3.MoveTowards(platform.position, targets[currentTarget].position, currentSpeed * Time.fixedDeltaTime);
	}


}
