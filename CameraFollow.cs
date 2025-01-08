using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    public float smoothingSpeed = 0.125f;

    public Vector3 offset;

    Vector3 followVelocity;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate ()
    {
        Vector3 desiredPosition = playerTransform.position + offset;

        Vector3 smoothedPostion = Vector3.SmoothDamp(transform.position, desiredPosition, ref followVelocity, smoothingSpeed * Time.deltaTime);

        transform.position = smoothedPostion;
    }
}
