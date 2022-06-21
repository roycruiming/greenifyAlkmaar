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
    }



    private float interpolFraction = 0.1f; 

    private void FixedUpdate()
    {

        Vector3 interpolResult = waypoints.GetPosition(destinationWayPoint, interpolFraction);
        
        LookAtButIgnoreYaxis(interpolResult); 

        _rigidbody.MovePosition(
               Vector3.MoveTowards(this.transform.position, interpolResult , 2f * Time.fixedDeltaTime));


        if (Vector3.Distance(transform.position, destinationWayPoint.position) < 0.1) {
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            interpolFraction = 0;      
        }

        else if (Vector3.Distance(transform.position, interpolResult) < 0.01) {
            interpolFraction = interpolFraction + 0.1f;
        }


        
    }


    //look at the next waypoint
    private void LookAtButIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
}


