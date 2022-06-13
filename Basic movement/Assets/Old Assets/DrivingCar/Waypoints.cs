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
    public int amountOfPointsCurve = 50;

    private void Awake()
    {
        //default is circular. 
        if (RouteType == RouteType.Undefined) {
            RouteType = RouteType.circular;     
        }
    }

    private void DrawWireFrames() {

        foreach (Transform t in transform)
        {
           Gizmos.color = Color.blue;
            
            if (IsCurve(t))
            {
                Gizmos.color = Color.blue; 
                Gizmos.DrawWireSphere(t.position, 1f);
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(t.GetChild(0).position, 1f);
                Gizmos.color = Color.blue;
            }
            else {

                
                //Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(t.position, 1f);
                //Gizmos.color = Color.blue;
            }
        }
    }


    private void OnDrawGizmos()
    {
        DrawWireFrames(); 
        for (int i = 0; i < transform.childCount - 1; i++) {
            Transform waypoint = transform.GetChild(i);
            Transform neighbour = transform.GetChild(i + 1);
            DrawGizmoLine(waypoint, neighbour); 
        }

        if (RouteType != RouteType.circular) return;
        DrawGizmoLine(transform.GetChild(transform.childCount -1) , transform.GetChild(0));

    }

    public void DrawGizmoLine(Transform from, Transform towards) {
        //if the line is from a curve, draw small spheres to form a dotted line.

        

        if (IsCurve(from))
        {
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
                float t = i / (float)amountOfPointsCurve;
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(CalculateQuadraticBezierPoint(t, from.position, from.GetChild(0).position, towards.position), 0.1f);
            }
            // from = from.GetChild(2);        
        }
        else {
            Gizmos.color = Color.blue; 
            Gizmos.DrawLine(from.position, towards.position);
        }
    
        //if the line is drawn towards a curve, connect to the first child instead of the father 

        
    }

    public Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float uSquared = u * u; 
        float tSquared = t * t;
        Vector3 p = uSquared * p0 + 2 * u * t * p1 + tSquared * p2;
        return p; 
    }


    public bool IsFirstChild(Transform wayPoint) {
        return wayPoint == transform.GetChild(0); 
    }

    public Transform GetNextWayPoint(Transform currentWaypoint)
    {
        //
        if (currentWaypoint == null || currentWaypoint.GetSiblingIndex() >= transform.childCount-1){
            return transform.GetChild(0);
        }
        else
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
    }

    public bool IsCurve(Transform t)
    {
        return t.childCount == 1;         
    }



}
