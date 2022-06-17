using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public float walkAnimationSpeed; 
    public Animator animator;
    public  Rigidbody _rigidbody;
    

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 8f;

    //the waypoint the npc is walking towards
    private Transform destinationWayPoint;
    //the path between waypoints the npc is traversing. 
    private List<Vector3> currentRoute;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public Transform StartingWaypoint;




    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();

        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);

    }


    private void LookAtIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }


    private void FixedUpdate()
    {

        if (currentRoute.Count > 0)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentRoute[0], moveSpeed * Time.fixedDeltaTime));
            if (Vector3.Distance(transform.position, currentRoute[0]) < 0.5)  currentRoute.RemoveAt(0);
        }

        else
        {
            destinationWayPoint = waypoints.GetNextWaypoint(destinationWayPoint); 
            currentRoute = waypoints.GetRouteTowardsNextWaypoint(destinationWayPoint); 
            
        }



    }    
    
}
