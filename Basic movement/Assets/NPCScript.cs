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

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        Move();
    }



    List<Vector3> curvePoints = new List<Vector3>();




    //TODO: IMPROVE THIS CODE.
    private void MoveCurve() {
        Transform curveControl = currentWaypoint.GetChild(0);


           
        //initiate curvePoints 
        if (curvePoints.Count == 0 )
        {
            for (int i = 1; i < 50 + 1; i++)
            {
                float t = i / (float)50;
                Vector3 pointPos = waypoints.CalculateQuadraticBezierPoint(t, currentWaypoint.position, curveControl.position, waypoints.GetNextWayPoint(currentWaypoint).position);
                pointPos.y = waypoints.GetNextWayPoint(currentWaypoint).position.y; 
                curvePoints.Add(pointPos);

            }

   

        }

        else if (curvePoints.Count > 0)
        {
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.deltaTime));

            if (Vector3.Distance(transform.position, curvePoints[0]) < 0.5)
            {
                transform.LookAt(curvePoints[0]);
                curvePoints.RemoveAt(0);
            }

            if (curvePoints.Count == 0)
            {
                currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            }
        }
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

        else rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.5)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            Lookat(); 
        }
    }


    private void Lookat() {
        Vector3 lookhere = currentWaypoint.position;
        transform.LookAt(lookhere);
    }


    private void Awake()
    {
        animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);

        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        else { currentWaypoint = StartingWaypoint; }

        transform.position = currentWaypoint.position;
 
        Lookat();



    }


}
