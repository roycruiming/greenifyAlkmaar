using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftUp : MonoBehaviour
{
    float x;
    float y = 0;
    float z;
    bool up = true;
    bool down = false;
    public GameObject lift;
    public MultiPlayerHandler MPH;
    // Start is called before the first frame update
    void Start()
    {
        lift = GameObject.Find("lift");
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
       
        x = lift.transform.position.x;
        z = lift.transform.position.z;
        lift.transform.position = new Vector3(x, MPH.y, z);
    }

    private void OnTriggerStay(Collider other)
    {
        print("test");
        MPH.MoveVork();
    }
        
   /* private void MoveVork()
    {
        if (y < 2f && up)
        {
            y += 0.01f;
        }
        if(y >= 2)
        {
            up = false;
            down = true;

        }
        if(y> 0f && down)
        {
            y -= 0.01f;
        }
        if(y <= 0)
        {
            up = true;
            down = false;
        }*/
        
    //}
    
}
