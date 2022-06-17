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
    private Transform destenationWayPoint;
    //the path between waypoints the npc is traversing. 
    private List<Vector3> currentRoute;
    //the waypoint that functions as the spawn/ startingwaypoint 
    public Transform StartingWaypoint;




    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }


    private void LookAtIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }


     private IEnumerator Move() {

        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);


        while (true)
        {
            //path isnt traversed
            if (currentRoute.Count > 0)
            {
                _rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentRoute[0], moveSpeed * Time.fixedDeltaTime));
                currentRoute.RemoveAt(0); 
            }

            //path is traversed, set next destenationWaypoint and get next route,   
            else {
                currentRoute = waypoints.GetRouteTowardsNextWaypoint(); 
            }  
        }
    }
}
