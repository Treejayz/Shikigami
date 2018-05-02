using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public enum CollectableType
    {
        PIECE,
        SCRAP,
        PAGE
    };

    public CollectableType Type = CollectableType.PIECE;

    public float spinSpeed;

    float acceleration = 100f;
    Transform player;

    private void Start()
    {

        transform.Rotate(0f, Random.Range(0,360), 0f);
    }

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.transform;
            StartCoroutine("Collect");
        }
       
    }

    IEnumerator Collect()
    {
        float speed = 0f;
        while (Vector3.Distance(player.position, transform.position) > 0.2f)
        {
            speed += Time.fixedDeltaTime * acceleration;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("Kill");
    }

    IEnumerator Kill()
    {
        switch (Type)
        {
				case (CollectableType.PIECE):
				if (CollectableManager.paperPieces == 0) {
					GetComponentInParent<FirstScrapCheck> ().Display ();
				}
                CollectableManager.Collect(Type);
                GetComponent<ParticleSystem>().Play();
                GetComponent<Collider>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Component halo = GetComponent("Halo");
                halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                AkSoundEngine.PostEvent("Pickup", gameObject);
                break;

            case (CollectableType.SCRAP):
                CollectableManager.Collect(Type);
                GetComponent<ParticleSystem>().Play();
                GetComponent<Collider>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                AkSoundEngine.PostEvent("PickupCoin", gameObject);

                break;

        };
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
