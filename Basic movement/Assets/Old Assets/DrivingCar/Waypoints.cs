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
    public RouteType RouteType = RouteType.circular;
    public int amountOfPointsCurve = 50;



    //draw wireframes of the route
    private void DrawWireFrames() {
        foreach (Transform t in transform)
        {
           Gizmos.color = Color.blue;
            
            //if curve (gameobject is parent of child)
            if (IsCurve(t))
            {
                Gizmos.DrawWireSphere(t.position, 1f);
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(t.GetChild(0).position, 1f);
                
            }
            else {
                Gizmos.DrawWireSphere(t.position, 1f);
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
    private void DrawGizmoLine(Transform from, Transform towards) {

        //if the line is from a curve, draw small spheres to form a dotted line.
        if (IsCurve(from))
        {
            foreach (Vector3 coordinate in GetRouteTowardsWaypoint(towards)) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(coordinate, 0.1f); 
            }     
        }
        else {
            Gizmos.color = Color.blue; 
            Gizmos.DrawLine(from.position, towards.position);
        }        
    }


    //dont ask me about math details, but this works
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float uSquared = u * u; 
        float tSquared = t * t;
        Vector3 p = uSquared * p0 + 2 * u * t * p1 + tSquared * p2;
        return p; 
    }
    public bool IsFirstChild(Transform wayPoint) {
        return wayPoint == transform.GetChild(0); 
    }

    public bool IsCurve(Transform fromWaypoint) {
        return !(fromWaypoint.childCount == 0);
    }


 

    //Returns the next waypoint
    public List<Vector3> GetRouteTowardsWaypoint(Transform destinationWaypoint)
    {
        List<Vector3> list = new List<Vector3>();

        //int currentWayPointIndex = destinationWaypoint.GetSiblingIndex();
        int currentIndex = destinationWaypoint.GetSiblingIndex() - 1;
        if (currentIndex == -1) { 
            currentIndex = transform.childCount - 1; }


        Transform currentWaypoint = transform.GetChild(currentIndex);


        //If the waypoint contains another gameobject...
        if (IsCurve(currentWaypoint)) 
        {
             //... this gameobject will act as a controlpoint for the curve
            Transform curveControlPoint = currentWaypoint.GetChild(0); 
            
            //break up the line in 
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
                float fractionOfLine = i / (float)amountOfPointsCurve;
                Vector3 pointPos = CalculateQuadraticBezierPoint(fractionOfLine, currentWaypoint.position, curveControlPoint.position, destinationWaypoint.position);
                list.Add(pointPos);
            }
        }

        else
        {
            //when the 
            transform.GetChild(0);
            //when route to next waypoint is empty
            list.Add(destinationWaypoint.position);
        }
        return list; 
    }


    //returns the next waypoint, or the first one when null in parameter. 
    public Transform GetFirstOrNextWayPoint(Transform currentWaypoint = null)
    {
        if (currentWaypoint == null || currentWaypoint.GetSiblingIndex() >= transform.childCount - 1)
        {
            return transform.GetChild(0);
        }
        else
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
    }
}



