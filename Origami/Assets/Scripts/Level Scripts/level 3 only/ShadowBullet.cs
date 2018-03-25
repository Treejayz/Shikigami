using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBullet : MonoBehaviour {

    public Vector3 direction;
    public GameObject target;

    public float speed;
    private float rotationspeed;
    private float rotateTime = 2f;

    private float targetRotX;
    private float xRate;
    private float targetRotY;
    private float yRate;
    private float targetRotZ;
    private float zRate;

    private bool dying;

    // Use this for initialization
    void Start () {
        targetRotX = Random.Range(50f, 300f);
        targetRotY = Random.Range(50f, 300f);
        targetRotY = Random.Range(50f, 300f);
        xRate = Random.Range(-300f, 300f);
        yRate = Random.Range(-300f, 300f);
        zRate = Random.Range(-300f, 300f);
        direction.Normalize();
        dying = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dying)
        {
            if (Vector3.Magnitude(transform.position - target.transform.position) < 1.5f && !target.GetComponent<Character>().dead)
            {
                target.GetComponent<Character>().SetState(new DeathState(target.GetComponent<Character>()));
                target.GetComponent<Character>().dead = true;
            }

            if (Mathf.Abs(yRate - targetRotY) < 10f)
            {
                if (targetRotY < 0f)
                {
                    targetRotY = Random.Range(50f, 200f);
                }
                else
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
            xRate = Mathf.Lerp(xRate, targetRotX, 2f * Time.deltaTime);
            yRate = Mathf.Lerp(yRate, targetRotY, 2f * Time.deltaTime);
            zRate = Mathf.Lerp(zRate, targetRotZ, 2f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!dying)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            transform.position = transform.position + direction * speed * Time.fixedDeltaTime;

            transform.Rotate(xRate * Time.fixedDeltaTime, yRate * Time.fixedDeltaTime, zRate * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
        StartCoroutine("Kill");
    }

    IEnumerator Kill()
    {
        dying = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        //GetComponent<Rigidbody>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }

}
