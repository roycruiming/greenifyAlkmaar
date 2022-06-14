using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public float walkAnimationSpeed; 
    public Animator animator;
    private new Rigidbody rigidbody;
    

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 8f;
    private Transform currentWaypoint;
    public Transform StartingWaypoint;
    public int amountOfPointsCurve = 50;


    private void Awake()
    {
        //if no startingwaypoint,  get "next waypoint from waypoints")
        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        //else currentwaypoint is starting waypoint
        else { currentWaypoint = StartingWaypoint; }

        //set npc at start waypoint 
        transform.position = currentWaypoint.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //animation
        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        //move the object
        Move();
    }

    private void Move()
    {
        //if routetype is linear && the currentwaypoint is the first waypoint, teleport the player 
        //to the first waypoint
        if (waypoints.RouteType == RouteType.Linear && waypoints.IsFirstChild(currentWaypoint))
        {
            transform.position = currentWaypoint.position;
        }

        //if waypoint is a curve (contains a child), use method movecurve and return
        if (waypoints.IsCurve(currentWaypoint))
        {
            MoveCurve();
            return;
        }

        // else move to next waypoint
        else
        {

            LookAtIgnoreY(currentWaypoint.position);
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.fixedDeltaTime));

        }

        // if waypoint is close set next waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.5)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
    }






    private List<Vector3> curvePoints = new List<Vector3>();

    private void MoveCurve() {
        //
        Transform curveControl = currentWaypoint.GetChild(0);
        //initiate curvePoints 
        
        //if no points in list 
        if (curvePoints.Count == 0 )
        {
            //divide line into points
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
               
                //not really a percentage but I dont know the word for a number like "0.25" 
                float CurveProgress = i / (float)amountOfPointsCurve;
                

                //curve is drawn between the entry (currentwaypoint.position, curvecontrol and the next waypoint)
                Vector3 pointPos = waypoints.CalculateQuadraticBezierPoint(CurveProgress, currentWaypoint.position, curveControl.position, waypoints.GetNextWayPoint(currentWaypoint).position); 
                curvePoints.Add(pointPos);
            } 
        }


        // if point count is above 0 
        else if (curvePoints.Count > 0)
        {
            //move npc to position
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.fixedDeltaTime));

            //if player is close to point, look at next curvepoint and remove point from list
            if (Vector3.Distance(transform.position, curvePoints[0]) < 0.5)
            {
                Vector3 lookat = curvePoints[0];
                curvePoints.RemoveAt(0);
                LookAtIgnoreY(lookat);
            }

            //if curve is finished (no points left in list), set next waypoint as current 
            if (curvePoints.Count == 0)
            {
                currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
                LookAtIgnoreY(currentWaypoint.position); 
            }
        }
    }




    //look at vector but ignore y-axis 
    private void LookAtIgnoreY(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
        
    }

    




}
