using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtons : MonoBehaviour {

    public Text text;
    Color startColor;

    public Image image;
    Color imageColor;


    float currentAlpha = 0f;

    private void Start()
    {
        startColor = text.color;
        Color temp = startColor;
        temp.a = 0f;
        text.color = temp;

        imageColor = image.color;
        temp = imageColor;
        temp.a = 0f;
        image.color = temp;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine("FadeIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn()
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

            currentAlpha += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }

        currentAlpha = 1f;
        temp = startColor;
        temp.a = currentAlpha;
        text.color = temp;

        temp = imageColor;
        temp.a = currentAlpha;
        image.color = temp;
    }

    IEnumerator FadeOut()
    {
        Color temp;
        while (currentAlpha > 0f)
        {
            temp = startColor;
            temp.a = currentAlpha;
            text.color = temp;

            temp = imageColor;
            temp.a = currentAlpha;
            image.color = temp;

            currentAlpha -= Time.deltaTime * 2f;
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
