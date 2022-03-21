using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public IntventoryObject inventory;

    public void doshit(Collider other)
    {
        Destroy(other.gameObject);
        var item = other.GetComponent<Item>();

        if (inventory.AddItem(item.item, 1))
        {
            Destroy(other.gameObject);
        }
        else
        {

            GameObject i = inventory.Container[0].item.prefab;
            inventory.Container.Clear();
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);

            Vector3 position = other.transform.position;
            UnityEngine.Quaternion quat = other.transform.rotation;
            Instantiate(i, position, quat);
        }

    }





    private void OnTriggerEnter(Collider other)
    {

      
        Destroy(other.gameObject);

        //other.

        var item = other.GetComponent<Item>();


  
        //other.gameObject

        if (inventory.AddItem(item.item, 1))
        {
            Destroy(other.gameObject);
        }
        else {
           
            GameObject i = inventory.Container[0].item.prefab;
            inventory.Container.Clear();
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
            Vector3 position = other.transform.position;

            GameObject player = GameObject.Find("CubeMe"); 

            Vector3 playerPos = player.transform.position;
            Vector3 playerDirection = player.transform.forward;
            Quaternion playerRotation = player.transform.rotation;
            float spawnDistance = 10;

            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

            //Vector3 x = new Vector3(position.x + 10, position.y, position.z + 10);
            UnityEngine.Quaternion quat = other.transform.rotation;
            Instantiate(i, spawnPos, quat);
        }




    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear(); 
    }
}


