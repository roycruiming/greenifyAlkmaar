using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public IntventoryObject inventory;

    public void AddOrSwap(Collider other)
    {

        

        var item = other.GetComponent<Item>();

        if (item.item.getprefab() == null) {
            item.item.setprefab(other.gameObject);

        }        

       

        if (inventory.AddItem(item.item, 1))
        {
            inventory.Container[0].item.setprefab(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else
        {

            //NORMAAL SWAPTE DIT, MAAR ik heb mn script gesloopt,
            //en IN DE PoC hoeft dit niet nog niet te werken 

            //GameObject i = inventory.Container[0].item.getprefab(); 
            //inventory.Container.Clear();
            //inventory.AddItem(item.item, 1);
            //other.gameObject.SetActive(false); 

            //Vector3 position = other.transform.position;
            //position.y = 0f; 
            //UnityEngine.Quaternion quat = other.transform.rotation;
            //Instantiate(i, position, quat);
        }

    }





  

    private void OnApplicationQuit()
    {
        inventory.Container.Clear(); 
    }

    public void Clear()
    {
        inventory.Container.Clear();
    }
}


