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

        //Vector3 diff = transform.position - (target.transform.position + targetMovementOffset);
        // Vector3 vel = body.velocity;

        //Vector3 force = (diff * -springForce) - (vel * springDamper);

        //body.AddForce(force);

        //transform.Position(cameraPositoin);
        transform.LookAt(target.transform.position /*+ targetLookAtOffset*/);
    }
}

