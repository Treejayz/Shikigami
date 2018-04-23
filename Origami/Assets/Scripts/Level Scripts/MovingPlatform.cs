using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public enum Type {LOOP, YOYO, OTHER};
    public Type PlatformType = Type.LOOP;

    private bool Yoyoing = false;

    public Transform[] targets;
    public Transform platform;

    public float moveTime = 6f;

    private int prevTarget;
    private int currentTarget;

    private int iteration = 0;

    private float progress;

	// Use this for initialization
	void Start () {
        platform.position = targets[0].position;
        currentTarget = 1;
        prevTarget = 0;
        iteration = (int)((Time.time / moveTime) - (Time.time % moveTime));
    }

    private void FixedUpdate()
    {
        if ((int)((Time.time / moveTime) - ((Time.time % moveTime)/moveTime)) > iteration)
        {
            iteration += 1;

            switch (PlatformType)
            {
                case Type.LOOP:
                    currentTarget += 1;
                    prevTarget += 1;
                    if (currentTarget == targets.Length)
                    {
                        currentTarget = 0;
                    }
                    if (prevTarget == targets.Length)
                    {
                        prevTarget = 0;
                    }
                    break;
                case Type.YOYO:
                    if (Yoyoing)
                    {
                        currentTarget -= 1;
                        prevTarget -= 1;
                        if (currentTarget == -1)
                        {
                            currentTarget = 1;
                            prevTarget = 0;
                            Yoyoing = false;
                        }
                    }
                    else
                    {
                        currentTarget += 1;
                        prevTarget += 1;
                        if (currentTarget == targets.Length)
                        {
                            currentTarget -= 2;
                            Yoyoing = true;
                        }
                    }
                    break;
            }
            platform.position = targets[prevTarget].position;
        }
        float temp = (Time.time % moveTime) / moveTime;
        progress = (Mathf.Cos(temp * Mathf.PI) + 1) / 2;
        platform.position = targets[currentTarget].position * (1 - progress) + targets[prevTarget].position * progress;
    }

    //void FixedUpdate () {
        
	//}


}
