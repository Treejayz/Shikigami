using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBullet : MonoBehaviour {

    public Vector3 target;
    public float speed;
    private float rotationspeed;

    private float rotateTime = 2f;

    private float targetRotX;
    private float xRate;
    private float targetRotY;
    private float yRate;
    private float targetRotZ;
    private float zRate;

    // Use this for initialization
    void Start () {
        targetRotX = Random.Range(50f, 300f);
        targetRotY = Random.Range(50f, 300f);
        targetRotY = Random.Range(50f, 300f);
        xRate = Random.Range(-300f, 300f);
        yRate = Random.Range(-300f, 300f);
        zRate = Random.Range(-300f, 300f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(yRate - targetRotY) < 10f)
        {
            if (targetRotY < 0f)
            {
                targetRotY = Random.Range(50f, 200f);
            } else
            {
                targetRotY = Random.Range(-200f, -50f);
            }
        }
        if (Mathf.Abs(xRate - targetRotX) < 10f)
        {
            if (targetRotX < 0f)
            {
                targetRotX = Random.Range(50f, 200f);
            }
            else
            {
                targetRotX = Random.Range(-200f, -50f);
            }
        }
        if (Mathf.Abs(zRate - targetRotZ) < 10f)
        {
            if (targetRotZ < 0f)
            {
                targetRotZ = Random.Range(50f, 200f);
            }
            else
            {
                targetRotZ = Random.Range(-200f, -50f);
            }
        }
        xRate = Mathf.Lerp(xRate, targetRotX, 1f * Time.deltaTime);
        yRate = Mathf.Lerp(yRate, targetRotY, 1f * Time.deltaTime);
        zRate = Mathf.Lerp(zRate, targetRotZ, 1f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        transform.Rotate(xRate * Time.fixedDeltaTime, yRate * Time.fixedDeltaTime, zRate * Time.fixedDeltaTime);
    }
}
