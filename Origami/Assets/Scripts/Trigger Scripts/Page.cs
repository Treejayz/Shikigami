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
            AkSoundEngine.PostEvent("BigPickup", other.gameObject);

            if (isFrog)
            {
                other.gameObject.GetComponent<Character>().canFrog = true;
                if (!other.gameObject.GetComponent<Character>().dead)
                {
                    other.gameObject.GetComponent<Character>().SetState(new TransformState(other.gameObject.GetComponent<Character>(), false));
                }
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<CapsuleCollider>().enabled = false;
                print("hi");
                CollectableManager.storybookpages[0] = true;
                //Destroy(this);
            }
            else if (isFox)
            {
                other.gameObject.GetComponent<Character>().canFox = true;
                if (!other.gameObject.GetComponent<Character>().dead)
                {
                    if (other.gameObject.GetComponent<Character>().Form == Character.CurrentForm.FROG)
                    {
                        other.gameObject.GetComponent<Character>().SetState(new TransformState(other.gameObject.GetComponent<Character>(), false));
                    }
                    else
                    {
                        other.gameObject.GetComponent<Character>().SetState(new TransformState(other.gameObject.GetComponent<Character>(), true));
                    }
                }
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<CapsuleCollider>().enabled = false;
                CollectableManager.storybookpages [2] = true;
                //Destroy(this);
            }

            else
            {
                Destroy(this.gameObject);
            }          
        }
    }
}
