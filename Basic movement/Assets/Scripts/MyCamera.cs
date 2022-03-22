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

        float vertical = Input.GetAxis("Mouse Y") * 3;

        if (transform.rotation.eulerAngles.x > 10 || transform.rotation.eulerAngles < -10)
        {
            print("cap here");
        }

        else {
            print(transform.rotation.x);
        }



    }
}

