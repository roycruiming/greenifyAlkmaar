using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasControls : MonoBehaviour
{

    public Camera cam; 

    // Start is called before the first frame update
    private void Update()
    {

        this.gameObject.transform.LookAt(cam.transform); 
    }
}
