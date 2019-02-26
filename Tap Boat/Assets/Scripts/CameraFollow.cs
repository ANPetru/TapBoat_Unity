using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private const float CAMERAXOFFSET = 3f;

    public Transform target;
    PlayerMovement player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        player = target.GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {

        Vector3 desiredPosition = target.position + new Vector3(AddXOffset(),offset.y,offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
        AddXOffset();
    }

    private float AddXOffset()
    {
        switch (player.GetXOffset())
        {
            case 0:
                return 0f;
            case -1:
                return -CAMERAXOFFSET;
            case 1:
                return CAMERAXOFFSET;
        }
        return 0;
    }

}
