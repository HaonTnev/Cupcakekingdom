using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CharacterController characterController;
    // movement
    public float speed = 5f;
    public float turnSpeed = 4f;
    public float currentSpeed = 0;

    public Vector3 nextPosition;
    //rotation
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    
    public float rotX;
    //gravity
    public float gravity = 9.87f;
    public float verticalSpeed = 0f;

    //shaking
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float timeElapsed = 10f;


    void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * speed);
        Move();
        MouseAiming();
        Screenshake();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (characterController.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        characterController.Move(Time.deltaTime * speed * move + gravityMove * Time.deltaTime);

        currentSpeed = horizontal;
    }

    void MouseAiming()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }

    void Screenshake()
    {
        if (timeElapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
            float z = originalPosition.z;

            transform.position = new Vector3(x, y, z);
            timeElapsed += Time.deltaTime;
        }
    }


    public void StartShake()
    {
        originalPosition = transform.position;
        timeElapsed = 0f;
    }
}
