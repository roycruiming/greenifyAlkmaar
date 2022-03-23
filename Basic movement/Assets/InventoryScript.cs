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

        item.item.prefab = other.gameObject; 

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





  

    private void OnApplicationQuit()
    {
        inventory.Container.Clear(); 
    }
}


