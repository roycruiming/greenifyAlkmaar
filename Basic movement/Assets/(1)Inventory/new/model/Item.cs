using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName = "inventory/storable item")]
public class Item : ScriptableObject
{
    private GameObject GameObject;

    public Sprite HudImage;


    

    public GameObject GetGameObject() {

        return this.GameObject; 
    }

    public void setGameObject(GameObject g) {
        this.GameObject = g;
    }

    

}