using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    private Transform currentWaypoint;

    Transform GetCurrentWayPoint() {
        return currentWaypoint; 
    }
    

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.position = currentWaypoint.position;  
        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);

        //transform.LookAt(currentWaypoint); 
        lookat(currentWaypoint); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f) {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            //transform.LookAt(currentWaypoint);
            lookat(currentWaypoint);
        }
    }

    private void lookat(Transform current) {
       // print(current.name);
        transform.LookAt(current);
        
        transform.transform.rotation *= Quaternion.Euler(0, -90, 0);
        // Vector3 angles = currentWaypoint.transform.eulerAngles;
        // Vector3 onlyYaxisAngles = new Vector3(0f, angles.y - 90, 0f);
        // currentWaypoint.transform.eulerAngles = onlyYaxisAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject == GameObject.Find("CubeMe"))
        {
            float speed = 600;
            //rigidBody.isKinematic = false;
            Vector3 force = transform.forward;
            force = new Vector3(force.x, 1, force.z);
            //rigidBody.AddForce(force * speed);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(force * 600);
        }

        else
        {
            
        }


    }

}
