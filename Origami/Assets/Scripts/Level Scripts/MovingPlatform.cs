using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public enum Type {LOOP, YOYO, OTHER};
    public Type PlatformType = Type.LOOP;

    private bool Yoyoing = false;

    public Transform[] targets;
    public Transform platform;
    public float speed;

    private int current;

	// Use this for initialization
	void Start () {
        platform.position = targets[0].position;
        current = 1;
	}

    private void Update()
    {
        if (Vector3.Distance(platform.position, targets[current].position) < 0.1f)
        {
            switch(PlatformType)
            {
            case Type.LOOP:
                current += 1;
                if (current == targets.Length)
                {
                    current = 0;
                }
                break;
                case Type.YOYO:
                if (Yoyoing)
                {
                    current -= 1;
                    if (current == -1)
                    {
                        current = 1;
                        Yoyoing = false;
                    }
                } else
                {
                    current += 1;
                    if (current == targets.Length)
                    {
                        current -= 2;
                        Yoyoing = true;
                    }
                }
                break;
            }
        }
    }

    void FixedUpdate () {
        platform.position = Vector3.MoveTowards(platform.position, targets[current].position, speed * Time.fixedDeltaTime);
	}


}
