using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public float walkAnimationSpeed; 
    public Animator animator;
    public new Rigidbody rigidbody;
    

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 8f;
    private Transform currentWaypoint;
    public Transform StartingWaypoint;
    public int amountOfPointsCurve = 50;

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        Move();
    }

    List<Vector3> curvePoints = new List<Vector3>();


    //TODO: IMPROVE THIS CODE.
    private void MoveCurve() {
        //first item in the curve is curvecontrol 
        Transform curveControl = currentWaypoint.GetChild(0);
        //initiate curvePoints 
        if (curvePoints.Count == 0 )
        {
            for (int i = 1; i < amountOfPointsCurve + 1; i++)
            {
                float t = i / (float)amountOfPointsCurve;
                Vector3 pointPos = waypoints.CalculateQuadraticBezierPoint(t, currentWaypoint.position, curveControl.position, waypoints.GetNextWayPoint(currentWaypoint).position); 
                curvePoints.Add(pointPos);
            } 
        }

        else if (curvePoints.Count > 0)
        {
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.fixedDeltaTime));

            if (Vector3.Distance(transform.position, curvePoints[0]) < 0.5)
            {
                Vector3 lookat = curvePoints[0];
                curvePoints.RemoveAt(0);
                LookAt(lookat);
            }

            if (curvePoints.Count == 0)
            {
                currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
                LookAt(currentWaypoint.position); 
            }
        }
    }

    private void LookAt(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
        
    }

    private void Move()
    {
        if (waypoints.RouteType == RouteType.Linear && waypoints.IsFirstChild(currentWaypoint))
        {
            transform.position = currentWaypoint.position;
        }

        if (waypoints.IsCurve(currentWaypoint))
        {
            MoveCurve();
            return;
        }

        else {

            LookAt(currentWaypoint.position); 
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.fixedDeltaTime)); 

        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.5)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
    }


    private void Awake()
    {
        animator.SetFloat("Vertical", 1, 0.1f, Time.fixedDeltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);

        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        else { currentWaypoint = StartingWaypoint; }

        transform.position = currentWaypoint.position;
    }


}
