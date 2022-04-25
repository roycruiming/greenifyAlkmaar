using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    private Transform currentWaypoint;
    public Transform StartingWaypoint; 

    


    Transform GetCurrentWayPoint() {
        return currentWaypoint; 
    }
    

    // Start is called before the first frame update
    void Start()
    {
        if (StartingWaypoint == null)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        }
        else { currentWaypoint = StartingWaypoint; }

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
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);;
            lookat(currentWaypoint);
        }
    }

    private void lookat(Transform current) {
        transform.LookAt(current);
        transform.transform.rotation *= Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject == GameObject.Find("MainCharacter"))
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
