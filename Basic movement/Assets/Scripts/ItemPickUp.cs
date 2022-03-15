using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public float pickUpRange = 5;
    public float moveForce = 250;
    public Transform holdParent;
    public Transform startRay;
    public Transform forward;
    private GameObject heldObj;


    private void Start()
    {   
        
    }
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(startRay.position, forward.position, out hit))
                {
                    if (hit.transform.name == "CubeMe")
                    {
                       
                    }
                    else
                    {
                        PickupObject(hit.transform.gameObject);
                        Debug.Log(hit.transform.name);
                    }
                }
                Debug.DrawLine(startRay.position, forward.position);
            }
            else
            {
                DropObject();
            }
        }

        if(heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = true;
            objRig.drag = 10;
            objRig.isKinematic = false;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        Rigidbody heldrig = heldObj.GetComponent<Rigidbody>();
        heldrig.useGravity = true;
        heldrig.drag = 1;
        heldrig.isKinematic = false;

        heldObj.transform.parent = null;
        heldObj = null;
    }
}
