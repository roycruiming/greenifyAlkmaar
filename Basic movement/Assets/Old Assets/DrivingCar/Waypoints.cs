using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RouteType
{
     Linear, circular
}

public class Waypoints : MonoBehaviour
{
    public RouteType RouteType =  RouteType.circular;
    public int AmountOfCurvePointsDrawed = 20; 

    //draw wireframes of the route
    private void DrawWireFrames() {
        foreach (Transform transform in transform)
        {
          

           Gizmos.color = Color.blue;
            
            //if transform has child, that means it has a curvecontrol point
            if (transform.childCount > 0 && RoutTypeIsLinearAndTransformIsFirstInHierarchy(transform))
            {
                Gizmos.DrawWireSphere(transform.position, 1f);
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.GetChild(0).position, 1f);
                
            }
            else {
                Gizmos.DrawWireSphere(transform.position, 1f);
            }
        }
    }
    private void OnDrawGizmos()
    {
       //first draw all wireframes 
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
        if (from.childCount > 0)
        {
            //... this gameobject will act as a controlpoint for the curve
            Transform curveControlPoint = from.GetChild(0);

            Gizmos.color = Color.red; 
            //break up the line in the amount of points
            for (int i = 1; i < AmountOfCurvePointsDrawed + 1; i++)
            {
                float fractionOfLine = i / (float)AmountOfCurvePointsDrawed;
                Vector3 pointPos = CalculateQuadraticBezierPoint(fractionOfLine, from.position, curveControlPoint.position, towards.position);
                Gizmos.DrawSphere(pointPos, 0.1f); 
            }
        }
        else {
            Gizmos.color = Color.blue; 
            Gizmos.DrawLine(from.position, towards.position);
        }        
    }

    //calculate nex
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float uSquared = u * u; 
        float tSquared = t * t;
        Vector3 p = uSquared * p0 + 2 * u * t * p1 + tSquared * p2;
        return p; 
    }


    public Vector3 GetInterpolatedPosition(Transform destinationWaypoint, float t)
    {

        Transform  currentWaypoint =  FindCurrentWaypoint(destinationWaypoint);

        //no children means linear interpol
        if (currentWaypoint.childCount == 0)
        {
            return Vector3.Lerp(currentWaypoint.position, destinationWaypoint.position, t);
        }
        //else return quadratic bezier 
        else {
            return CalculateQuadraticBezierPoint(t, currentWaypoint.position, currentWaypoint.GetChild(0).position, destinationWaypoint.position);        
        }
    }

    private Transform FindCurrentWaypoint(Transform destinationWaypoint)
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

    public bool RoutTypeIsLinearAndTransformIsFirstInHierarchy(Transform t)
    {
        return this.RouteType == RouteType.Linear && t == transform.GetChild(0); 
    }
}



