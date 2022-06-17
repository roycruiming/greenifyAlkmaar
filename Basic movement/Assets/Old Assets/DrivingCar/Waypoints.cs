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
            
            //if curve
            if (IsCurve(t))
            {
                Gizmos.DrawWireSphere(t.position, 1f);
                //curvecontrol must be black 
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
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
                float t = i / (float)amountOfPointsCurve;
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(CalculateQuadraticBezierPoint(t, from.position, from.GetChild(0).position, towards.position), 0.1f);
            }       
        }
        else {
            Gizmos.color = Color.blue; 
            Gizmos.DrawLine(from.position, towards.position);
        }        
    }





    //dont ask me about math details, but this works
    public Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        float u = 1 - t;
        float uSquared = u * u; 
        float tSquared = t * t;
        Vector3 p = uSquared * p0 + 2 * u * t * p1 + tSquared * p2;
        return p; 
    }

    
    //checks if child is the first child
    public bool IsFirstChild(Transform wayPoint) {
        return wayPoint == transform.GetChild(0); 
    }



    public Transform GetNextWaypoint(Transform currentWayPoint) {
        throw new NotImplementedException();     
    }
 

    //Returns the next waypoint
    public List<Vector3> GetRouteTowardsNextWaypoint(Transform nextWaypoint)
    {
        return new List<Vector3>();
    }

    public bool IsCurve(Transform t)
    {
        return t.childCount == 1;         
    }




}


//
//private void MoveLine()
//{
//    //move rigid body towards the waypoint 
//    _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentDestination.position, moveSpeed * Time.fixedDeltaTime));

//    //if npc is nearby the currentwaypoint set a new one... 
//    if (Vector3.Distance(transform.position, currentDestination.position) < 0.5)
//    {
//        currentDestination = waypoints.GetNextCoordinates(currentDestination);
//        //..and also look at the new waypoint (but ignore the y-axis.
//        LookAtIgnoreYaxis(currentDestination.position);
//    }
//}






//private List<Vector3> curvePoints = new List<Vector3>();

//private void MoveCurve() {
//    //first item in the curve is the controlpoint 
//    //Transform curveControl = currentDestination.GetChild(0);

//    //if the curveline isn't broken into multiple fragments 
//    if (curvePoints.Count == 0 )
//    {
//        for (int i = 1; i < amountOfPointsCurve + 1; i++)
//        {
//            //didnt know how to name this variable.. naming could be incorrect mathematically 
//            float fractionOfLine = i / (float)amountOfPointsCurve;

//            //by looking at the current waypoint (the position of the curvecontrol and the next waypoint, a point in a curve is calculated and added to the list,
//            //a point is saved to the curvepoints list
//            Vector3 pointPos = waypoints.CalculateQuadraticBezierPoint(fractionOfLine, currentDestination.position, curveControl.position, waypoints.GetNextCoordinates(currentDestination).position); 
//            curvePoints.Add(pointPos);
//        } 
//    }

//    //if there are more than one points left. 
//    else if (curvePoints.Count > 0)
//    {
//        //move towards the first point 
//        _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.fixedDeltaTime));

//        // if npc comes close to curvepoint
//        if (Vector3.Distance(transform.position, curvePoints[0]) < 0.5)
//        {
//            //lookat 
//            Vector3 lookat = curvePoints[0];
//            curvePoints.RemoveAt(0);
//            LookAtIgnoreYaxis(lookat);
//        }

//        //if npc completed the curve
//        if (curvePoints.Count == 0)
//        {
//            //look at next
//            currentDestination = waypoints.GetNextCoordinates(currentDestination);
//           //LookAtIgnoreYaxis(currentDestination.position); 
//        }
//    }
//}



