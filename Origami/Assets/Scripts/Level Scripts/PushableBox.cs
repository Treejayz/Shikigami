using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour {

    bool moving;
    bool returning;
    float speed = 7f;

    Vector3 direction;
    Vector3 targetpos;
    Vector3 initialpos;

    Vector3 baseDirection;

    private void Start()
    {
        moving = false;
        returning = false;
        baseDirection = transform.forward;
    }

    
    void FixedUpdate () {
		if (moving)
        {
            if (Vector3.Distance(transform.position, targetpos) < 0.03f)
            {
                transform.position = targetpos;
                moving = false;
                returning = false;
            } else
            {
                transform.position = Vector3.Lerp(transform.position, targetpos, speed * Time.fixedDeltaTime);
            }
        }
	}

    public void Hit(ControllerColliderHit hit)
    {
        if (!moving)
        {
            direction = hit.normal * -1f;
            direction.y = 0;
            float angle = Vector3.Angle(baseDirection, direction);
            if (angle < 45f)
            {
                direction = baseDirection;
            }
            else if (angle < 135f)
            {
                if (Vector3.Angle(transform.right, direction) < 90f)
                {
                    direction = transform.right;
                }
                else
                {
                    direction = transform.right * -1f;
                }
            }
            else
            {
                direction = baseDirection * -1f;
            }
            targetpos = transform.position + direction * 4f;
            initialpos = transform.position;
            if (!Physics.BoxCast(transform.position, new Vector3(1.95f, 1.95f, 1.95f), direction, Quaternion.identity, 4f)) {
                moving = true;
            }
        }
          
    }

    public void OnCollisionEnter(Collision collision)
    {
        print("ouch");
        if (!returning && moving)
        {
            print("return");
            targetpos = initialpos;
            initialpos = transform.position;
            returning = true;
        }
        if(collision.collider.tag == "Player")
        {
        }        
    }
}
