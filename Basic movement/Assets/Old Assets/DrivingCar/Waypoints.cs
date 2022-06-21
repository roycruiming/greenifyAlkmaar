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
    public int amountOfPointsCurve = 20;

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
            //for () { 
            
            //}
            //foreach (Vector3 coordinate in GetRouteTowardsWaypoint(towards)) {
            //    Gizmos.color = Color.red;
            //    Gizmos.DrawSphere(coordinate, 0.1f); 
            //}     
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
    private bool IsFirstChild(Transform wayPoint) {
        return wayPoint == transform.GetChild(0); 
    }

    private bool IsCurve(Transform fromWaypoint) {
        return !(fromWaypoint.childCount == 0);
    }

    public Vector3 GetPosition(Transform destinationWaypoint, float t)
    {
        

        Transform  currentWaypoint =  FindCurrentWaypoint(destinationWaypoint);

        if (!IsCurve(currentWaypoint))
        {
            return Vector3.Lerp(currentWaypoint.position, destinationWaypoint.position, t);
        }
        else {
            return CalculateQuadraticBezierPoint(t, currentWaypoint.position, currentWaypoint.GetChild(0).position, destinationWaypoint.position);        
        }
    }

    Transform FindCurrentWaypoint(Transform destinationWaypoint)
    {
        int currentIndex = destinationWaypoint.GetSiblingIndex() - 1;
        if (currentIndex == -1) { currentIndex = transform.childCount - 1; }
        return transform.GetChild(currentIndex);
    }


    //returns the next waypoint, or the first one when parameter is null. 
    public Transform GetFirstOrNextWayPoint(Transform currentWaypoint = null)
    {
        if (currentWaypoint == null || currentWaypoint.GetSiblingIndex() >= transform.childCount - 1) return transform.GetChild(0);
        else return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
    }

    public bool IsRouteTypeLinearAndDestinationFirstChildInHierarchy(Transform t)
    {
        return this.RouteType == RouteType.Linear && t == transform.GetChild(0); 
    }
}



