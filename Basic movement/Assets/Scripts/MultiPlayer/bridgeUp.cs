using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeUp : MonoBehaviour
{

    public GameObject bridge;

    public bool bridgeUps;

    // Start is called before the first frame update
    void Start()
    {
        bridge = GameObject.Find("bridge-road-hill");
        bridge.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        bridgeUps = true;
     }

    private void OnTriggerStay(Collider other)
    {
        bridgeUps = true;
    }

    private void OnTriggerExit(Collider other)
    {
        bridgeUps = false;
    }


}
