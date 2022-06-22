using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPCScript : MonoBehaviour
{
    
    [SerializeField] private float walkAnimationSpeed;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float MovementSpeed = 4f;
    [SerializeField] private float CurveSharpness = 20;
    private Animator animator;
    private Rigidbody _rigidbody;
    //the waypoint the npc is walking towards
    public Transform destinationWayPoint;
    public Transform StartingWaypoint;
    private const double MaxDistance = 0.3;
     
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SetupAnimation();
        SpawnAndSetFirstDestination();
    }

    private void SetupAnimation()
    {
        if (animator == null) return;
        animator.SetFloat("WalkSpeed", walkAnimationSpeed);
        animator.SetFloat("Vertical", 1);
    }

    private void SpawnAndSetFirstDestination()
    {
        //if startingwaypoint is null get startingwaypoint from getnextwaypoint method, and spawn player there 
        if (StartingWaypoint == null) this.transform.position = waypoints.GetFirstOrNextWayPoint().position;
        //else spawn on starting wayingpoint.  
        else this.transform.position = StartingWaypoint.transform.position;
        //set destination on next waypoint
        destinationWayPoint = waypoints.GetFirstOrNextWayPoint(StartingWaypoint);
    }

    private float interpolFraction = 0f; 
    private void FixedUpdate()
    {
        //if the destination is the first waypoint in the hierarchy, and 
        //the route type is linear,  teleport player to first waypoint
        if (waypoints.RoutTypeIsLinearAndTransformIsFirstInHierarchy(destinationWayPoint))
        {
            interpolFraction = 0;
            _rigidbody.position = destinationWayPoint.position;
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            return;
        }

        //find the next position to walk to. 
        Vector3 interpolatedPosition = waypoints.GetInterpolatedPosition(destinationWayPoint, interpolFraction);
        //look at the position (but ignore y)
        LookAtButIgnoreYaxis(interpolatedPosition);
        //move towards the position 
        _rigidbody.MovePosition(
               Vector3.MoveTowards(this.transform.position, interpolatedPosition , MovementSpeed * Time.fixedDeltaTime));
        
        //if close towards destination
        if (Vector3.Distance(transform.position, destinationWayPoint.position) < MaxDistance) {
            //set fraction to zero
            interpolFraction = 0; 
            //go to next waypoint
            destinationWayPoint = waypoints.GetFirstOrNextWayPoint(destinationWayPoint);
            
        }
        //else if not close to destination, but close to the interpolated point...
        else if (Vector3.Distance(transform.position, interpolatedPosition) < MaxDistance) {
            //... Add to interpolFraction
            interpolFraction = interpolFraction +  1/CurveSharpness;
        }
    }
    //look at the next waypoint
    private void LookAtButIgnoreYaxis(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
}


