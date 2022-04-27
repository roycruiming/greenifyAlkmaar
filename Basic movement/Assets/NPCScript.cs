using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public float walkAnimationSpeed; 
    public Animator animator;
    public new Rigidbody rigidbody;
    //private Vector3 endPos;
    private float elapsedTime;
    private float desiredDuration = 10;

    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 2f;
    private Transform currentWaypoint;
    public Transform StartingWaypoint;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        Move();

    }


    private void Move()
    {
        if (waypoints.RouteType == RouteType.Linear && waypoints.IsFirstChild(currentWaypoint))
        {
            transform.position = currentWaypoint.position;
        }
        else
        {
            rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime));
        }



        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            Lookat(); 

        }
    }


    private void Lookat() {
        Vector3 lookhere = currentWaypoint.position;
        lookhere.y = transform.position.y;
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


        //endPos = GameObject.Find("WalkHere").transform.position;
        if (!animator) { gameObject.GetComponent<Animator>(); }
        if (!rigidbody) { gameObject.GetComponent<Animator>(); }

    }


}
