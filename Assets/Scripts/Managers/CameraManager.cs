using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float targetHeight = 0; // Initialize to the initial height
    private float moveSpeed = 10.0f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    public void FollowFrosty(bool isGoingUp)
    {
        if (isGoingUp)
        {
            targetHeight = 11.0f;
        }
        else
        {
            targetHeight = 0.0f;
        }
    }

    void Update()
    {
        targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

        // Move the camera towards the target height
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
