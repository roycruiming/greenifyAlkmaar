using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> target;
    public static int objectivesCounter = 0;



    private void Update()
    {
        Vector3 targetPosition = target[objectivesCounter].transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }

    public void NextObjective()
    { 
        objectivesCounter++;
    }
}
