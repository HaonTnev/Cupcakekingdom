using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schlange : MonoBehaviour
{
    private Animator mAnimator;
    public float rotaspeed = 5f;
    Vector3 currentEulerAngles;
    float x;
    float y;
    float z;
    //public GameObject schlange;
    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame

    void Update()
    {
       if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                mAnimator.SetTrigger("played");
            }
        }
        
        while(true)
        {
            y = Time.deltaTime * 5f;
        }
       
        //if (Input.GetKeyDown(KeyCode.X)) x = 1 - x;
      
       // if (Input.GetKeyDown(KeyCode.Y)) y = 1 - y;
       
       // if (Input.GetKeyDown(KeyCode.Z)) z = 1 - z;

        //modifying the Vector3, based on input multiplied by speed and time
        //currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotaspeed;

        //apply the change to the gameObject
       // transform.eulerAngles = currentEulerAngles;

    }
}