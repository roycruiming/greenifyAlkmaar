using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MainMapCharacter : MonoBehaviour
{

    int currentWaypoint;
    List<MainmapWaypoint> Path;
    int targetIndex;
    int currentIndex;
    public float characterSpeed = 1;
    bool directionIsForward;

    List<MainmapWaypoint> allWaypointObjects;

    public void Awake() {
        Path = new List<MainmapWaypoint>();
        targetIndex = 0; //moet nog dynamisch maken
        currentIndex = 0;
        currentWaypoint = 0;

        allWaypointObjects = new List<MainmapWaypoint>(GameObject.FindObjectsOfType<MainmapWaypoint>());
        allWaypointObjects = allWaypointObjects.OrderBy(x => x.index).ToList(); //sort the list by index
        directionIsForward = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWayPoint();
    }

    public void MoveToWayPoint() {
        // Transform target = GameObject.Find("level-selector-outline").transform;
        // Vector3 targetPos = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
        // Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        // transform.position = smoothPos;
        if(Path.Count > 0) {
            Vector3 targetPos = new Vector3(Path[0].x,transform.position.y,Path[0].z);
            Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, characterSpeed * Time.deltaTime);
            transform.position = smoothPos;
            Path.RemoveAt(0);
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
            for (int i = currentIndex; i > targetIndex; i--)
            {
                Path.Add(allWaypointObjects[i]);
            }
        }
    }

    private int findWayPointByLevelIndex(int index) {
        foreach(MainmapWaypoint m in this.allWaypointObjects) {
            if(m.isLevel && m.levelIndex == index) return m.index;
        }

        return -1;
    }
}
