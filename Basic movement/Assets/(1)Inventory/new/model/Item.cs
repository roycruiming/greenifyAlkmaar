using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//[CreateAssetMenu(menuName = "inventory/storable item")]
public class Item : MonoBehaviour
{

    public float rotationOffsetY = 0;
    public float heightOffsetY = 0; 

    private GameObject GameObject;

    public Sprite HudImage;

    //private Transform InitPos; 

    private void Awake()      
    {
        this.GameObject = this.gameObject;
        this.heightOffsetY = this.gameObject.transform.position.y; 
        //this.InitPos = this.gameObject.transform; 
    }

    public GameObject GetGameObject() {
        return this.GameObject; 
    }

    public void setGameObject(GameObject g) {
        this.GameObject = g;
    }
}