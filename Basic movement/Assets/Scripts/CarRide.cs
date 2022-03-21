using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRide : MonoBehaviour
{
    public Transform target;
    public float speed;

    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.MoveTowards(a, b, speed);

        Destroy(gameObject, 11f);
    }

    

    
}
