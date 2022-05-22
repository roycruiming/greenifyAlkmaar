using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class RotateCharacter : MonoBehaviour
{


    private GameObject player;
    public bool IsSecondPlayer; 

    private Vector3 startingRotation;
    //private List<GameObject> children = new List<GameObject>(); 

    // Start is called before the first frame update


    private void Awake()
    {
        player = GameObject.Find("3RD Person");

        startingRotation = player.transform.rotation.eulerAngles;
        
    }


    public void rotate(float r) {
        r = -r;
        player.transform.localEulerAngles = new Vector3(0f, startingRotation.y + 180 +  r * 180, 0f);   
        }












}
