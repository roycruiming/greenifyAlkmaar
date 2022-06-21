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
    //list of positions  
    private List<Vector3> CoordinatesTowardsDestination;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public Transform StartingWaypoint;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        SetupAnimation();
        InitFirstRoute();
    }

    private void SetupAnimation()
    {
        if (animator == null) return;
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        animator.SetFloat("Vertical", 1);
    }


    private void InitFirstRoute()
    {
        //if startingwaypoint is null get startingwaypoint from getnextwaypoint method, and spawn player there 
        if (StartingWaypoint == null)
            this.transform.position = waypoints.GetFirstOrNextWayPoint().position;
        //else spawn on starting wayingpoint.  
        else
            this.transform.position = StartingWaypoint.position;
        //set destination on next waypoint
        destinationWayPoint = waypoints.GetFirstOrNextWayPoint(StartingWaypoint);
        //ask for the route towards the destination waypoint. 
        CoordinatesTowardsDestination = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
    }


    private void FixedUpdate()
    {
        //if route is linear and destination is first waypoint in hierarchy
        if (waypoints.IsRouteTypeLinearAndIsDestinationFirstChildInHierarchy(destinationWayPoint)) {
            //teleport player to destination
            transform.position = destinationWayPoint.position;
            CoordinatesTowardsDestination.Clear(); 
        }
        // if the path toward the next waypoint is completed. 
         if (CoordinatesTowardsDestination.Count == 0)
        {
            //set next destination
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            //and ask for for a new path 
            CoordinatesTowardsDestination = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
        }
        //look at the coordinates but ignore height 
        LookAtButIgnoreYaxis(CoordinatesTowardsDestination[0]);
        //move npc towards coordinates; 
        _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, CoordinatesTowardsDestination[0], moveSpeed * Time.fixedDeltaTime));
        //if player is close remove coordinate from list 
        if (Vector3.Distance(transform.position, CoordinatesTowardsDestination[0]) < 0.1)
            CoordinatesTowardsDestination.RemoveAt(0);
    }


    //look at the next waypoint
    private void LookAtButIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
}


