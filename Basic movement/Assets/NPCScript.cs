using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPCScript : MonoBehaviour
{
    [SerializeField] private float walkAnimationSpeed;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 8f;
    private Animator animator;
    private Rigidbody _rigidbody;
    //the waypoint the npc is walking towards
    private Transform destinationWayPoint;
    //the path between waypoints the npc is traversing towards the next waypoint. 
    private List<Vector3> currentRoute;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public waypoint StartingWaypoint;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        SetupAnimation();
        InitRoute();
    }

    private void SetupAnimation()
    {
        if (animator == null) return;
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        animator.SetFloat("Vertical", 1);
    }


    private void InitRoute()
    {
        //if startingwaypoint is null..., 
        if (StartingWaypoint == null)
        {
            //... get startingwaypoint from getnextwaypoint method, and spawn player there
            this.transform.position = waypoints.GetFirstOrNextWayPoint().position;
        }
        else
        {
            //else spawn on starting wayingpoint.  
            this.transform.position = StartingWaypoint.transform.position;
        }
        //set destination on next waypoint
        destinationWayPoint = waypoints.GetFirstOrNextWayPoint(StartingWaypoint.transform);
        //ask for the road towards the destination waypoint. (straight line if no curve, curve if curve) 
        currentRoute = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
    }


    private void FixedUpdate()
    {

        if (waypoints.MustTeleportBack(destinationWayPoint)) {
            transform.position = destinationWayPoint.position;
            currentRoute.Clear(); 
        }

        // if the path toward the next waypoint is traversed, 
         if (currentRoute.Count == 0)
        {
            //set next destination
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            //ask for next route towards destination
            currentRoute = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
        }

        //look at the coordinates but ignore height 
        LookAtButIgnoreYaxis(currentRoute[0]);
        //move npc towards coordinates; 
        _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentRoute[0], moveSpeed * Time.fixedDeltaTime));
        //if player is close remove coordinate from list 
        if (Vector3.Distance(transform.position, currentRoute[0]) < 0.1)
            currentRoute.RemoveAt(0);
    }


    //look at the next waypoint
    private void LookAtButIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
}


