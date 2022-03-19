using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDynaScript : MonoBehaviour
{
    public GameObject recycleSprite;
    public GameObject gasSprite;
    public GameObject factorySprite;
    public GameObject windmillSprite;
    public GameObject solarSprite;

    // Start is called before the first frame update
    void Start()
    {
        GameObject testImage = Instantiate(windmillSprite, Vector3.zero, Quaternion.identity) as GameObject;
        testImage.transform.SetParent(transform, false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
