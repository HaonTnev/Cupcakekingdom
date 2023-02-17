using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascension : MonoBehaviour
{

    public GameObject target;
    public GameObject cameraMain;

    public float speed = 5.0f;
    public float currentSpeed;
    public float frequency = 1.0f;
    public float amplitude = 1.0f;
    public float waituntilascension;
    public float waituntildestruction;

    private float startTime;
    public bool rise = false;

    void Start()
    {
        startTime = Time.time;
        StartCoroutine(Timer1());
        StartCoroutine(Timer2());

    }

    void Update()
    {
        if (rise) Ascend();
    }

    void Ascend()
    {
        float time = Time.time - startTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > 0.1f)
        {
            Vector3 direction = target.transform.position - transform.position;
            Vector3 normal = Vector3.Cross(direction, Vector3.up).normalized;
            Vector3 tangent = Vector3.Cross(normal, direction).normalized;
            Vector3 offset = tangent * amplitude * Mathf.Tan(frequency * time);

            currentSpeed = speed + amplitude * Mathf.Tan(frequency * time);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 15);

            transform.position += direction.normalized * currentSpeed * Time.deltaTime + offset;
        }

        if(time > waituntildestruction)
        {
            Destroy(this.gameObject);
        } 
    }

    IEnumerator Timer2()
    {
        yield return new WaitForSeconds(waituntilascension);
        Debug.Log("Ascension");
        rise = true;
        cameraMain.GetComponent<CameraMovement>().StartShake();
    }

    IEnumerator Timer1()
    {
        yield return new WaitForSeconds(waituntilascension - 3.5f);
        cameraMain.GetComponent<CameraMovement>().StartShake();
    }
}
