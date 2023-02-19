using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float timeElapsed = 0f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (timeElapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
            float z = originalPosition.z;

            transform.position = new Vector3(x, y, z);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    public void StartShake()
    {
        originalPosition = transform.position;
        timeElapsed = 0f;
    }

}
