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


    private Animator animator;
    private Rigidbody _rigidbody;
    //the waypoint the npc is walking towards
    public Transform destinationWayPoint;
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
        if (StartingWaypoint == null) this.transform.position = waypoints.GetFirstOrNextWayPoint().position;
        //else spawn on starting wayingpoint.  
        else this.transform.position = StartingWaypoint.transform.position;
        //set destination on next waypoint
        destinationWayPoint = waypoints.GetFirstOrNextWayPoint(StartingWaypoint);
        //ask for the route towards the destination waypoint.
        //
    }



    private float t; 


    private void FixedUpdate()
    {

        t = (t + Time.fixedDeltaTime);




        Vector3 c = waypoints.GetPosition(destinationWayPoint, t);

        LookAtButIgnoreYaxis(c);

        _rigidbody.MovePosition(c);

        float dist = Vector3.Distance(transform.position, destinationWayPoint.position);

        if (Vector3.Distance(transform.position, destinationWayPoint.position) < 0.5)
        {
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);

            t = 0;

        }





    }


    //look at the next waypoint
    private void LookAtButIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
}


