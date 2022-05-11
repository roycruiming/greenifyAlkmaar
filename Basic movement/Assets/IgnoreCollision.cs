using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{

    public string tag;

    void Awake()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tag == collision.gameObject.tag) {

            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), collision.collider);
        }
    }

}
