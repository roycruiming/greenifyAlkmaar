using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DirectionalArrow : MonoBehaviour
{
    public ObjectivesController objCon;

    public GameObject[] objects;

    private float oldDistance = 9999;
    GameObject closetsObject;



    [System.Obsolete]


    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();

        //objects = GameObject.FindGameObjectsWithTag("Objectives");
        
    }

    private void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("Objectives");


        if (objects.Length != 0)
        {
            Vector3 targetPosition = GetClosestObjective(objects).transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }


    GameObject GetClosestObjective(GameObject[] objects)
    {
        foreach (GameObject g in objects)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closetsObject = g;
                oldDistance = dist;
            }
        }
        return closetsObject;
    }
}
