using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 offset;

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 desiredPosition = new Vector2(target.transform.position.x + offset.x, target.transform.position.y + offset.y);
        Vector2 this2DPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 smoothedPosition = Vector2.Lerp(this2DPos, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}