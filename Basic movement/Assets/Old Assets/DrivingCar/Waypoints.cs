using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RouteType
{
    Undefined, Linear, circular
}

public class Waypoints : MonoBehaviour
{


    public RouteType RouteType; 



    private void Start()
    {
        
    }


    private void Awake()
    {
        if (RouteType == RouteType.Undefined) {
            RouteType = RouteType.circular;     
        }
    }

    private void OnDrawGizmos()
    {

        //

        //print("ffs");

        foreach (Transform t in transform) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 1f); 
        }

        for (int i = 0; i < transform.childCount - 1; i++) {

            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position); 
            
        }

        if (RouteType != RouteType.circular) return; 
        Gizmos.DrawLine(transform.GetChild(transform.childCount -1).position , transform.GetChild(0).position);

        
    }

    public bool IsFirstChild(Transform wayPoint) {
        return wayPoint == transform.GetChild(0); 
    }






    public Transform GetNextWayPoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null){
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else {
            return transform.GetChild(0);
        }


        
    }
}
