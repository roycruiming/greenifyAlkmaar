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
    //the path between waypoints the npc is traversing. 
    private List<Vector3> currentRoute;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public Transform StartingWaypoint;


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
        if (StartingWaypoint == null)
        {
            this.transform.position = waypoints.GetFirstOrNextWayPoint().position;
        }
        else
        {
            this.transform.position = StartingWaypoint.position;
        }
        destinationWayPoint = waypoints.GetFirstOrNextWayPoint(StartingWaypoint);
        currentRoute = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
    }


    private void LookAtIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }


    private void FixedUpdate()
    {

        if (currentRoute.Count == 0)
        {
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            currentRoute = waypoints.GetRouteTowardsWaypoint(destinationWayPoint);
            return;
        }

        LookAtIgnoreYaxis(currentRoute[0]);
        _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentRoute[0], moveSpeed * Time.fixedDeltaTime));


        if (Vector3.Distance(transform.position, currentRoute[0]) < 0.1)
            currentRoute.RemoveAt(0);

    }
}


