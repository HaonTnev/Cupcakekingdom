using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public float torqueForce = 10.0f;
    public float minBounceAngle = 30.0f;

    private bool isBouncing = false;
    private Terrain terrain;

    void Start()
    {
        terrain = FindObjectOfType<Terrain>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isBouncing && collision.collider.CompareTag("MainCamera"))
        {
            Vector3 direction = transform.position - collision.transform.position;
            float angle = Vector3.Angle(direction, Vector3.up);

            if (angle > minBounceAngle)
            {
                Vector3 normal = collision.contacts[0].normal;
                Vector3 bounceDirection = Vector3.Reflect(direction, normal).normalized;
                GetComponent<Rigidbody>().AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * torqueForce, ForceMode.Impulse);

                isBouncing = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isBouncing = false;
    }

    void FixedUpdate()
    {
        if (isBouncing && terrain != null)
        {
            Vector3 rayOrigin = transform.position + Vector3.down * 10.0f;
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, Vector3.up, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                float terrainHeight = terrain.SampleHeight(hitInfo.point);
                if (hitInfo.point.y - terrainHeight < 1.0f)
                {
                    Vector3 normal = hitInfo.normal;
                    Vector3 bounceDirection = Vector3.Reflect(GetComponent<Rigidbody>().velocity, normal).normalized;
                    GetComponent<Rigidbody>().velocity = bounceDirection * GetComponent<Rigidbody>().velocity.magnitude;
                }
            }
        }
    }

}
