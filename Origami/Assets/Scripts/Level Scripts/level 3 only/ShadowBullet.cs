using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBullet : MonoBehaviour {

    public Vector3 direction;
    public Vector3 newDirection;
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
        StartCoroutine("CollectData");
    }
	
	// Update is called once per frame
	void Update () {
        if (!dying)
        {
            if (Vector3.Magnitude(transform.position - target.transform.position) < 1.5f && !target.GetComponent<Character>().dead)
            {
                target.GetComponent<Character>().SetState(new DeathState(target.GetComponent<Character>()));
                target.GetComponent<Character>().dead = true;
                StartCoroutine("Kill");
            } 
            //else if (Vector3.Magnitude(transform.position - target.transform.position) < 15f)
            //{
                //Vector3 targetDir = (target.transform.position - transform.position).normalized;
                //direction = Vector3.Lerp(direction, targetDir, 1f * Time.deltaTime);
            //}
            else {
                direction = Vector3.Lerp(direction, newDirection, .3f * Time.deltaTime);
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
        StartCoroutine("Kill");
    }

    IEnumerator CollectData()
    {
        List<Vector3> list = new List<Vector3>();

        Vector3 prev = target.transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 current = target.transform.position;
        Vector3 temp = (current - prev);
        Vector3 result = temp * 10f;
        newDirection = calculateTarget(result).normalized;
        StartCoroutine("CollectData");
    }

    private Vector3 calculateTarget(Vector3 velocity)
    {
        Vector3 toPlayer = (target.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(toPlayer, velocity);
        Vector3 uj = toPlayer * dot;
        Vector3 ui = velocity - uj;

        float viMag = ui.magnitude;
        float vjMag = Mathf.Sqrt(speed * speed - 9 - viMag * viMag);

        Vector3 vj = toPlayer * vjMag;

        Vector3 targetVec = ui + vj;

        return targetVec;
    }

    IEnumerator Kill()
    {
        dying = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }

}
