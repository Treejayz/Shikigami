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

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch(Type)
            {
                case (CollectableType.PIECE):
                    CollectableManager.Collect(Type);
                    GetComponent<ParticleSystem>().Play();
                    GetComponent<Collider>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    Component halo = GetComponent("Halo");
                    halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
                    StartCoroutine("Kill");
                    AkSoundEngine.PostEvent("Pickup", gameObject);
                    break;

                case (CollectableType.SCRAP):
                    CollectableManager.Collect(Type);
                    GetComponent<Collider>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    StartCoroutine("Kill");
                    AkSoundEngine.PostEvent("PickupCoin", gameObject);
                    break;

            };


            
        }
       
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
