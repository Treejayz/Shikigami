using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = -25.0f;
    private const float Y_ANGLE_MAX = 80.0f;

    public Transform target;
    [SerializeField]
    private static float currentX = 0.0f;
    [SerializeField]
    private static float currentY = 0.0f;
	public GameObject pause;

    private Camera cam;
    private float maxDistance = 10.0f;
    [HideInInspector]
    public static float newMaxDistance = 10f;
    private float currentDistance;
    private float sensX = 4.0f;
    private float sensY = 1.0f;
	private int layerMask;

	private Vector3[] targetArray = new [] 
	{ new Vector3(0f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 1f, 0f) };

    private static bool onWall;
    private static float wallAngle;
    private static float wallMin;
    private static float wallMax;

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
		if (!pause.GetComponent<PauseSystem> ().pause) {
			currentX += Input.GetAxis ("Mouse X") * sensX;
			currentY += Input.GetAxis ("Mouse Y") * sensY * -1f;
            newMaxDistance += Input.GetAxis("Mouse ScrollWheel") * -1f;
            newMaxDistance = Mathf.Clamp(newMaxDistance, 5f, 25f);

            maxDistance =  Mathf.Lerp(maxDistance, newMaxDistance, Time.deltaTime * 5f);
		}

        bool mouseMoved = true;
        if (Input.GetAxis("Mouse X") == 0)
        {
            mouseMoved = false;
        }

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        if (onWall && !mouseMoved)
        {
            if (Mathf.Abs(currentX - wallAngle) > 180)
            {
                if (currentX > wallAngle)
                {
                    currentX = Mathf.Lerp(currentX, wallAngle + 360, 0.1f);
                } else
                {
                    currentX = Mathf.Lerp(currentX, wallAngle - 360, 0.1f);
                }
            } else
            {
                currentX = Mathf.Lerp(currentX, wallAngle, 0.1f);
            }
            if (Mathf.Abs(currentX - wallAngle) < 2f)
            {
                onWall = false;
            }
        }
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
			Vector3 dir = new Vector3 (0, 0, -currentDistance);
			Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
			this.transform.position = target.position + rotation * dir;
			this.transform.LookAt (target.position);
    }

    public static void SetAngle()
    {
        Vector2 target = Checkpoint.GetAngle();
        currentX = target.y;
        currentY = target.x + 20f;
    }
    public static void Wall(Vector3 wallNormal, Vector3 position) 
    {
        if (Physics.Raycast(position, wallNormal,newMaxDistance))
        {
            bool test1 = false;
            bool test2 = false;
            Vector3 newAng1 = Quaternion.Euler(0, 90, 0) * wallNormal;
            float newx1 = Vector3.Angle(newAng1, new Vector3(0, 0, 1));
            if (newAng1.x < 0)
            {
                newx1 = 360f - newx1;
            }
            Vector3 newAng2 = Quaternion.Euler(0, -90, 0) * wallNormal;
            float newx2 = Vector3.Angle(newAng2, new Vector3(0, 0, 1));
            if (newAng2.x < 0)
            {
                newx2 = 360f - newx2;
            }
            if (!Physics.Raycast(position, newAng1,newMaxDistance))
            {
                test1 = true;
                
            }
            if (!Physics.Raycast(position, newAng2, newMaxDistance))
            {
                test2 = true;
            }
            onWall = true;
            if (test1 && test2)
            {
                float temp1 = Mathf.Abs(currentX - newx1);
                float temp2 = Mathf.Abs(currentX - newx2);
                if (temp1 > temp2)
                {
                    wallAngle = newx2;
                } else
                {
                    wallAngle = newx1;
                }

            } else if (test1)
            {
                wallAngle = newx2;
            } else if (test2)
            {
                wallAngle = newx1;
            } else
            {
                onWall = false;
            }
        }

    }

    public static void OffWall()
    {
        onWall = false;
    }
}
