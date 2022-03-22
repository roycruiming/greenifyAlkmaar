using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmapWaypoint : MonoBehaviour
{
    public int index;
    public bool isLevel;

    public float x;
    public float y;
    public float z;

    public int levelIndex;
    int targetIndex;

    public void Awake() {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelSelectorHandler h = GameObject.Find("LevelHandlerScript").GetComponent<LevelSelectorHandler>();
    }
}
