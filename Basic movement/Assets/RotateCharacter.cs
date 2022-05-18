using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{

    public GameObject player;

    private Vector3 startingRotation;

    // Start is called before the first frame update


    private void Awake()
    {
        startingRotation = transform.rotation.eulerAngles;

    }


    public void rotate(float r) {

        r = -r; 


        print(startingRotation.y); 

        transform.localEulerAngles = new Vector3(0f, startingRotation.y +  r * 180, 0f);

        print(transform.localEulerAngles);

            
        }
        

        

         
        
    
       
        
    
}
