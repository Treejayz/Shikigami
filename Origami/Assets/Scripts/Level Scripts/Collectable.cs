using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public enum CollectableType
    {
        PIECE,
        PAGE
    };

    public CollectableType Type = CollectableType.PIECE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CollectableManager.Collect();
            GetComponent<ParticleSystem>().Play();
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine("Kill");
        }
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
