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
        yield return new WaitForSeconds(0.05f);
        time += 0.05f;
        while (time < 0.5f)
        {
            current = player.transform.position;
            Vector3 temp = (current - prev);
            list.Add(temp);
            prev = current;

            yield return new WaitForSeconds(0.05f);
            time += 0.05f;
        }

        Vector3 result = new Vector3(0f, 0f, 0f);
        int i = 0;

        foreach (Vector3 vec in list)
        {
            result += vec;
            i += 1; 
        }
        result = result / i;
        Vector3 target = calculateTarget(result);
        GameObject bullet = Instantiate(shadowBullet,transform.position, Quaternion.identity);
        bullet.GetComponent<ShadowBullet>().target = target;
        bullet.GetComponent<ShadowBullet>().speed = 15f;
    }

    private Vector3 calculateTarget(Vector3 velocity)
    {


        return (player.transform.position);
    }
}
