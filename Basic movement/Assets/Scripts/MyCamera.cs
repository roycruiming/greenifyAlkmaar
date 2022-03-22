using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject cameraPositoin;
    public GameObject target;

    public Vector3 targetMovementOffset;
    public Vector3 targetLookAtOffset;

    public float springForce;
    public float springDamper;

    void Start()
    {
        //
    }

    void FixedUpdate()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();

        print(transform.name);
        print(target.transform.name);

        //transform.position + 100;
        float vertical = Input.GetAxis("Mouse Y") * 5; 

        transform.Rotate(vertical, 0, 0); 

        
        transform.LookAt(target.transform);
    }
}

