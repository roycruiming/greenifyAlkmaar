using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public float walkAnimationSpeed; 
    public Animator animator;
    public new Rigidbody rigidbody;
    

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 8f;

    //the waypoint the target is walking towards
    private Transform currentWaypoint;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public Transform StartingWaypoint;
    //a curve is devided up into this amount of points 
    public int amountOfPointsCurve = 50;



    private void Awake()
    {
        //If no startingwaypoint is defined, get startingwaypoint from waypoints. 
        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        //Else set currentwaypoint as starting waypoint 
        else { currentWaypoint = StartingWaypoint; }

        //teleport to starting/current waypoint. 
        transform.position = currentWaypoint.position;
    }


    void FixedUpdate()
    {
        //set animation 
        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        //start moving the npc
        Move();
    }


    private void Move()
    {
        //  If routetype = linear and the currentwaypoint is the first child in the hierarchy (waypoint is firstwaypont
        if (waypoints.RouteType == RouteType.Linear && waypoints.IsFirstChild(currentWaypoint))
        {
            //teleport npc to first waypoint. 
            transform.position = currentWaypoint.position;
        }

        //if the waypoint has a child, that means that the npc needs to move in a curve. 
        if (waypoints.IsCurve(currentWaypoint)) MoveCurve();
        //else use MoveLine method for moving a straight line. 
        else MoveLine();
    }



    private void MoveLine()
    {
        //move rigid body towards the waypoint 
        rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.fixedDeltaTime));

        //if npc is nearby the currentwaypoint set a new one... 
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.5)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            //..and also look at the new waypoint (but ignore the y-axis.
            LookAtIgnoreYaxis(currentWaypoint.position);
        }
    }






    private List<Vector3> curvePoints = new List<Vector3>();

    private void MoveCurve() {
        //first item in the curve is the controlpoint 
        Transform curveControl = currentWaypoint.GetChild(0);
        
        //if the curveline isn't broken into multiple fragments 
        if (curvePoints.Count == 0 )
        {
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
                //didnt know how to name this variable.. naming could be incorrect mathematically 
                float fractionOfLine = i / (float)amountOfPointsCurve;
                
                //by looking at the current waypoint (the position of the curvecontrol and the next waypoint, a point in a curve is calculated and added to the list,
                //a point is saved to the curvepoints list
                Vector3 pointPos = waypoints.CalculateQuadraticBezierPoint(fractionOfLine, currentWaypoint.position, curveControl.position, waypoints.GetNextWayPoint(currentWaypoint).position); 
                curvePoints.Add(pointPos);
            } 
        }

        //if there are more than one points left. 
        else if (curvePoints.Count > 0)
        {
            //move towards the first point 
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.fixedDeltaTime));

            // if npc comes close to curvepoint
            if (Vector3.Distance(transform.position, curvePoints[0]) < 0.5)
            {
                //lookat 
                Vector3 lookat = curvePoints[0];
                curvePoints.RemoveAt(0);
                LookAtIgnoreYaxis(lookat);
            }

            //if npc completed the curve
            if (curvePoints.Count == 0)
            {
                //look at next
                currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
                LookAtIgnoreYaxis(currentWaypoint.position); 
            }
        }
    }

    private void LookAtIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
        
    }



}
