using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//[CreateAssetMenu(menuName = "inventory/storable item")]
public class Item : MonoBehaviour
{


    private GameObject GameObject;

    public Sprite HudImage;

    //private Transform InitPos; 

    private void Awake()      
    {
        this.GameObject = this.gameObject;
        //this.InitPos = this.gameObject.transform; 
    }

    public GameObject GetGameObject() {
        return this.GameObject; 
    }

    public void setGameObject(GameObject g) {
        this.GameObject = g;
    }
}