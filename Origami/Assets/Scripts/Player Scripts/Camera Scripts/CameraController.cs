using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = -25.0f;
    private const float Y_ANGLE_MAX = 80.0f;

    public Transform target;
    [SerializeField]
    private float currentX = 0.0f;
    [SerializeField]
    private float currentY = 0.0f;

    private Camera cam;
    private float maxDistance = 10.0f;
    private float currentDistance;
    private float sensX = 4.0f;
    private float sensY = 1.0f;
	private int layerMask;

	private Vector3[] targetArray = new [] 
	{ new Vector3(0f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 1f, 0f) };

    // Use this for initialization
    private void Start () {
        cam = Camera.main;
		layerMask = 1 << 8;
		layerMask = ~layerMask;
		for (int i = 0; i < 4; i++) {
			targetArray[i].z = cam.nearClipPlane;
		}
	}
	
	// Update is called once per frame
	private void Update () {
        currentX += Input.GetAxis("Mouse X") * sensX;
        currentY += Input.GetAxis("Mouse Y") * sensY * -1f;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        if (currentX < 0.0f) { currentX += 360; }
        if (currentX > 360.0f) { currentX -= 360; }

        RaycastHit hit;

		currentDistance = maxDistance;
		for (int i = 0; i < 4; i++) {
			Vector3 currentPos = cam.ViewportToWorldPoint(targetArray[i]);
			if (Physics.Raycast(target.position, (currentPos - target.position), out hit, maxDistance, layerMask))
			{
				if (currentDistance > hit.distance) {
					currentDistance = hit.distance;
				}
			} 
		}
        

	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -currentDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        this.transform.position = target.position + rotation * dir;
        this.transform.LookAt(target.position);
    }
}
