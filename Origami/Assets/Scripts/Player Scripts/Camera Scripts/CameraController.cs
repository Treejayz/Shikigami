using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 70.0f;

    public Transform target;

    private Camera cam;
    private float maxDistance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensX = 4.0f;
    private float sensY = 1.0f;

    // Use this for initialization
    private void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	private void Update () {
        currentX += Input.GetAxis("Mouse X") * sensX;
        currentY += Input.GetAxis("Mouse Y") * sensY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        if (currentX < 0.0f) { currentX += 360; }
        if (currentX > 360.0f) { currentX -= 360; }
	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -maxDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        this.transform.position = target.position + rotation * dir;
        this.transform.LookAt(target.position);
    }
}
