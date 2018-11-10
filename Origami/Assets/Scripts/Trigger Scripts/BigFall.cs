using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigFall : MonoBehaviour {

    public GameObject player;

    public Text text;
    Color startColor;

    public Image image;
    Color imageColor;

    float currentAlpha = 0f;

    Character playerCharacter;
    bool falling = false;
    Vector3 startPos;

    public bool active = false;

	// Use this for initialization
	void Start () {
        playerCharacter = player.GetComponent<Character>();

        startColor = text.color;
        Color temp = startColor;
        temp.a = 0f;
        text.color = temp;

        imageColor = image.color;
        temp = imageColor;
        temp.a = 0f;
        image.color = temp;

    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (!falling && playerCharacter.falling)
            {
                falling = true;
                startPos = player.transform.position;
            }
            if (falling)
            {
                if (!playerCharacter.falling)
                {
                    falling = false;
                    float fallDistance = startPos.y - player.transform.position.y;
                    if (fallDistance > 35f)
                    {
                        StopAllCoroutines();
                        StartCoroutine("Appear");
                    }
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = true;
        }
    }


    IEnumerator Appear()
    {
        Color temp;
        while (currentAlpha < 1f)
        {
            temp = startColor;
            temp.a = currentAlpha;
            text.color = temp;

            temp = imageColor;
            temp.a = currentAlpha;
            image.color = temp;

            currentAlpha += Time.deltaTime * 4f;
            yield return new WaitForEndOfFrame();
        }

        currentAlpha = 1f;
        temp = startColor;
        temp.a = currentAlpha;
        text.color = temp;
        temp = imageColor;
        temp.a = currentAlpha;
        image.color = temp;

        yield return new WaitForSeconds(3f);

        while (currentAlpha > 0f)
        {
            temp = startColor;
            temp.a = currentAlpha;
            text.color = temp;

            temp = imageColor;
            temp.a = currentAlpha;
            image.color = temp;

            currentAlpha -= Time.deltaTime * 4f;
            yield return new WaitForEndOfFrame();
        }
        currentAlpha = 0f;
        temp = startColor;
        temp.a = currentAlpha;
        text.color = temp;

        temp = imageColor;
        temp.a = currentAlpha;
        image.color = temp;
    }
}
