using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFire : MonoBehaviour {

    public GameObject shadowBullet;
    public GameObject player;

    float fireTime;

	// Use this for initialization
	void Start () {
        fireTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        fireTime += Time.deltaTime;
        if (fireTime > 1f)
        {
            StartCoroutine("CollectData");
            fireTime = 0f;
        }
	}

    IEnumerator CollectData()
    {
        float time = 0f;
        List<Vector3> list = new List<Vector3>();

        Vector3 prev = player.transform.position;
        Vector3 current;
        yield return new WaitForSeconds(0.1f);
        time += 0.1f;
        while (time < 0.5f)
        {
            current = player.transform.position;
            Vector3 temp = (current - prev);
            list.Add(temp);
            prev = current;

            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }

        Vector3 result = new Vector3(0f, 0f, 0f);
        float i = 0;

        foreach (Vector3 vec in list)
        {
            result += vec;
            i += 1; 
        }
        result = result / i;
        result = result * 10f;
        //print(result);
        Vector3 target = calculateTarget(result).normalized;
        GameObject bullet = Instantiate(shadowBullet,transform.position, Quaternion.identity);
        //Vector3 direction = target - transform.position;
        bullet.GetComponent<ShadowBullet>().direction = target;
        bullet.GetComponent<ShadowBullet>().target = player;
        bullet.GetComponent<ShadowBullet>().speed = 23f;
    }

    private Vector3 calculateTarget(Vector3 velocity)
    {

        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(toPlayer, velocity);
        Vector3 uj = toPlayer * dot;
        Vector3 ui = velocity - uj;

        float viMag = ui.magnitude;
        float vjMag = Mathf.Sqrt(25 * 25 - viMag * viMag);

        Vector3 vj = toPlayer * vjMag;

        Vector3 targetVec = ui + vj;

        return targetVec;
    }
}
