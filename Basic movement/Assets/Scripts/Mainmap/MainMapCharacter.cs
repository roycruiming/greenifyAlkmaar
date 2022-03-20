using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MainMapCharacter : MonoBehaviour
{

    MainmapWaypoint currentWaypoint;
    List<MainmapWaypoint> Path;
    int targetIndex;
    int currentIndex;
    float characterSpeed;
    bool directionIsForward;
    bool isMoving;
    MainmapWaypoint targetWaypoint;

    List<MainmapWaypoint> allWaypointObjects;

    public void Awake() {
        //default position is the meent level index 0
        Path = new List<MainmapWaypoint>();
        targetIndex = 0; //moet nog dynamisch maken
        currentIndex = 0;
        isMoving = false;
        characterSpeed = 160;
        

        allWaypointObjects = new List<MainmapWaypoint>(GameObject.FindObjectsOfType<MainmapWaypoint>());
        allWaypointObjects = allWaypointObjects.OrderBy(x => x.index).ToList(); //sort the list by index
        directionIsForward = true;

        currentWaypoint = allWaypointObjects[this.findWayPointByLevelIndex(0)];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        MoveToWayPoint();
    }
    public void MoveToWayPoint() {
        if(Path.Count > 0) {
            //check if the currentIndex is leveltargetIndex if so skip the whole path and clear it
            // if(currentIndex == targetIndex) {
            //     Path.Clear();
            //     targetWaypoint = null;
            // }

            //decide if the waypoint has been reached and set the next waypoint
            if(targetWaypoint == null) targetWaypoint = Path[0];

            //decide if there are alot of waypoints so increase the speed of the character until less waypoints are present
            //get the total of waypoints that exist
            int totalOfWayPoints = allWaypointObjects.Count;
            int amountOfCurrentWaypoints = Path.Count;
            if(amountOfCurrentWaypoints > (totalOfWayPoints * 2)) this.characterSpeed = 480;
            else if(amountOfCurrentWaypoints > totalOfWayPoints) this.characterSpeed = 280;
            else if(amountOfCurrentWaypoints > (totalOfWayPoints / 2)) this.characterSpeed = 220;
            else if(amountOfCurrentWaypoints > (totalOfWayPoints / 3)) this.characterSpeed = 200;
            else if(amountOfCurrentWaypoints > (totalOfWayPoints / 4)) this.characterSpeed = 180;
            else this.characterSpeed = 160; //default

            //move the character
            Vector3 targetPos = new Vector3(targetWaypoint.x, transform.position.y, targetWaypoint.z);
            Vector3 smoothPos = Vector3.MoveTowards(transform.position, targetPos, characterSpeed * Time.deltaTime);
            transform.position = smoothPos;
            isMoving = true;
            //end move character
            //turn character facing towards waypoint
            transform.LookAt(new Vector3(targetWaypoint.x, targetWaypoint.y, targetWaypoint.z));
            //


            //check if the waypoint has been reached if so set the next waypoint
            //Debug.Log("Target x , z :" + targetWaypoint.x + " " + targetWaypoint.y);
            //Debug.Log("Own x , z :" + transform.position.x + " " + transform.position.x);
            Vector3 difference = transform.position - new Vector3(targetWaypoint.x, transform.position.y, targetWaypoint.z);
            bool reachedDestination;
            //math abs make the number positive so going forward or backwards doesn't make a difference
            if(Mathf.Abs(difference.x) <= 5.5 && Mathf.Abs(difference.z) <= 5.5) reachedDestination = true;
            else reachedDestination = false;
            
            if(reachedDestination) {
                if(Path.Count > 0) currentWaypoint = Path[0]; //never used
                if(Path.Count > 0) currentIndex = Path[Path.Count - 1].index;
                if(Path.Count > 0) Path.RemoveAt(0);
                targetWaypoint = null;
            }
        }

    }

    public void AddPath(int levelIndex) {
        targetIndex = this.findWayPointByLevelIndex(levelIndex);
        Debug.Log("Target index: " + targetIndex);
        //find out if the target index is behind or
        if (targetIndex > currentIndex) this.directionIsForward = true; //move forward
        else this.directionIsForward = false; //move backward

        if (directionIsForward)
        {
            for (int i = 0; i < allWaypointObjects.Count; i++)
            {
                if(allWaypointObjects[i].index > currentIndex && allWaypointObjects[i].index <= targetIndex) {
                    Path.Add(allWaypointObjects[i]);
                }
            }
        }
        else
        {
            for (int i = currentIndex; i >= targetIndex; i--)
            {
                Debug.Log(i);
                Path.Add(allWaypointObjects[i]);
            }
        }
    }

    private int findWayPointByLevelIndex(int index) {
        foreach(MainmapWaypoint m in this.allWaypointObjects) {
            if(m.isLevel && m.levelIndex == index) {
                Debug.Log("Levelindex = " + m.index);
                return m.index;
            }
        }

        return -1;
    }
}
