using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftAz : MonoBehaviour
{

    float x;
    float y = 0;
    float z;
    bool up = true;
    bool down = false;
    public GameObject liftAza;
    public MultiPlayerHandler MPH;


    // Start is called before the first frame update
    void Start()
    {
        liftAza = GameObject.Find("CubeLift");
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        x = liftAza.transform.position.x;
        z = liftAza.transform.position.z;
        liftAza.transform.position = new Vector3(x, MPH.liftY, z);
    }

    private void OnTriggerStay(Collider other)
    {
        print("test");
        MPH.MoveLift();
    }
}
