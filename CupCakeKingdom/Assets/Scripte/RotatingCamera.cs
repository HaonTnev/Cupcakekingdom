using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 axis = Vector3.up;
    public float rotationSpeed = 1.0f;
    public Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        transform.RotateAround(target.position, axis, rotationSpeed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
