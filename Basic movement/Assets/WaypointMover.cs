using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    private Transform currentWaypoint;
    public Transform StartingWaypoint;
    public bool bloon;



    public void Move() {
        if (waypoints.RouteType == RouteType.Linear && waypoints.IsFirstChild(currentWaypoint))
        {
            transform.position = currentWaypoint.position;
        }

        if (waypoints.IsCurve(currentWaypoint))
        {
            MoveCurve();
            return;
        }

        else transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.5)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            lookat(currentWaypoint);
        }


    }


    List<Vector3> curvePoints = new List<Vector3>();

    private void MoveCurve()
    {
        Transform curveControl = currentWaypoint.GetChild(0);



        //initiate curvePoints 
        if (curvePoints.Count == 0)
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
            transform.position =  Vector3.MoveTowards(this.transform.position, curvePoints[0], moveSpeed * Time.deltaTime);

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






    // Start is called before the first frame update
    void Awake()
    {
        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        else { currentWaypoint = StartingWaypoint; }

        transform.position = currentWaypoint.position;  

        if (!bloon) { lookat(currentWaypoint);  }
         
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }



    private void lookat(Transform current) {
        transform.LookAt(current);
        transform.transform.rotation *= Quaternion.Euler(0, 0, 0);
    }




}
