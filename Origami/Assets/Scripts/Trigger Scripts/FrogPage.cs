using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPage : MonoBehaviour {

    public float spinSpeed;
    public float frequency;
    public float amplitude;

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
            other.gameObject.GetComponent<Character>().canFrog = true;
            Destroy(this.gameObject);
        }
    }
}
