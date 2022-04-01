using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DirectionalArrow : MonoBehaviour
{
    public ObjectivesController objCon;


    [System.Obsolete]


    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();
    }

    private void Update()
    {
        //Arrow points to next objective
        if (objCon.target.Count >= 1)
        {
            Vector3 targetPosition = objCon.target[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

        }
    }
}
