using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFire : MonoBehaviour {

    public GameObject shadowBullet;
    public GameObject shadowPillar;
    public GameObject player;
    public Animator dragonAnim;

    float fireTime;

    public enum FireType { BULLET, PILLARLEAD, PILLARSTILL, NONE };
    public FireType Type;

    // Used for pillarstill
    Vector3 playerPositionOld;
    bool recharge;


    // Use this for initialization
    void Start () {
        fireTime = 0f;
        recharge = false;
        playerPositionOld = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        fireTime += Time.deltaTime;
        switch(Type)
        {
            case FireType.BULLET:
                if (fireTime > 4f && Vector3.Distance(transform.position, player.transform.position) < 100f)
                {
                    StartCoroutine("Bullet");
                    fireTime = 0f;
                }
                break;
            case FireType.PILLARLEAD:
                if (fireTime > 3f && Vector3.Distance(transform.position, player.transform.position) < 100f)
                {
                    StartCoroutine("Pillar");
                    fireTime = 0f;
                }
                break;
            case FireType.PILLARSTILL:
                if (fireTime > .1f && !recharge)
                {
                    //if (Vector3.Distance(player.transform.position, playerPositionOld) < .1f)
                    if (player.GetComponent<Character>().momentum.magnitude < 1f)
                    {
                        RaycastHit hit;
                        int layerMask = 1 << 8;
                        layerMask = ~layerMask;
                        if (Physics.SphereCast(player.transform.position + Vector3.up * 3f, .5f, Vector3.down, out hit, 10f, layerMask))
                        {
                            GameObject pillar = Instantiate(shadowPillar, hit.point + Vector3.up * .01f, Quaternion.identity);
                            pillar.GetComponent<ShadowPillar>().buildTime = 1.5f;
                            pillar.transform.parent = hit.transform;
                            recharge = true;
                            fireTime = 0f;
                        }
                    } else
                    {
                        playerPositionOld = player.transform.position;
                    }
                    
                }
                if (fireTime > 10f && !recharge)
                {
                    //if (Vector3.Distance(player.transform.position, playerPositionOld) < .1f)
                    RaycastHit hit;
                    int layerMask = 1 << 8;
                    layerMask = ~layerMask;
                    if (Physics.SphereCast(player.transform.position + Vector3.up * 3f, .5f, Vector3.down, out hit, 10f, layerMask))
                    {
                        GameObject pillar = Instantiate(shadowPillar, hit.point + Vector3.up * .01f, Quaternion.identity);
                        pillar.transform.parent = hit.transform;
                        recharge = true;
                        fireTime = 0f;
                    }
                    else
                    {
                        playerPositionOld = player.transform.position;
                    }
                }

                if (fireTime > 5f && recharge)
                {
                    fireTime = 0f;
                    recharge = false;
                }
                break;
            case FireType.NONE:
                fireTime = 0f;
                break;
        };
	}

    IEnumerator Pillar()
    {

        Vector3 prev = player.transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 current = player.transform.position;
        Vector3 velocity = (current - prev) * 10f;
        Vector3 targetpos = player.transform.position + velocity;

        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.SphereCast(targetpos + Vector3.up * 40f, 2f, Vector3.down,out hit, 80f, layerMask)){
            GameObject pillar = Instantiate(shadowPillar, hit.point + Vector3.up * .01f, Quaternion.identity);
        }
    }

    IEnumerator Bullet()
    {
        dragonAnim.SetTrigger("Attacc");
        yield return new WaitForSeconds(0.55f);


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
        bullet.GetComponent<ShadowBullet>().dragon = transform;
        bullet.GetComponent<ShadowBullet>().speed = 27f;
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
