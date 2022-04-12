using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BirdGroupFlyaway : MonoBehaviour
{
    public GameObject flyingBird;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        var heading = this.transform.position - player.transform.position;
        if(heading.sqrMagnitude < 5*5)
        {
            
        }
        
    }

    void FlyAway()
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>();
        foreach(Transform bird in children)
        {
            GameObject flier = (GameObject)PrefabUtility.InstantiatePrefab(flyingBird);
            flier.transform.position = bird.transform.position;
            flier.transform.rotation = bird.transform.rotation;
            flier.transform.parent = bird.transform.parent;
            Destroy(bird);
        }
    }
}
