using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    public float spinSpeed;
    public float frequency;
    public float amplitude;

    public bool isFrog = false;
    public bool isFox = false;

    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        float tempY = startY;
        tempY += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = new Vector3(transform.position.x, tempY, transform.position.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isFrog)
            {
                other.gameObject.GetComponent<Character>().canFrog = true;
				CollectableManager.spellbookpages [0] = 1;
            }
            else if (isFox)
            {
                other.gameObject.GetComponent<Character>().canFox = true;
				CollectableManager.spellbookpages [2] = 1;
            }

            // Same sound as the normal checkpoints. Subject to change.
            AkSoundEngine.PostEvent("BigPickup", other.gameObject);

            Destroy(this.gameObject);
        }
    }
}
