using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class RotateCharacter : MonoBehaviour
{

    public GameObject player;

    private Vector3 startingRotation;
    //private List<GameObject> children = new List<GameObject>(); 

    // Start is called before the first frame update


    private void Awake()
    {
        startingRotation = transform.rotation.eulerAngles;
        //foreach (Transform c in GameObject.Find("_MainRig").transform.Find("Geometry").GetComponentsInChildren<Transform>()) children.Add(gameObject); 

       


    }


    public void rotate(float r) {

        r = -r; 


        print(startingRotation.y); 

        transform.localEulerAngles = new Vector3(0f, startingRotation.y +  r * 180, 0f);

        print(transform.localEulerAngles);

            
        }


    public void nextchar()
    {
        print(transform.name);

        
        List<GameObject> gameObjects = transform.GetComponentsInChildren<Transform>().Select(transform => transform.gameObject).ToList();

        //GameObject active = gameObjects.FirstOrDefault(x => x.activeInHierarchy);

        //int index = gameObjects.IndexOf(active);

        print("ff"); 

     




    }









}
