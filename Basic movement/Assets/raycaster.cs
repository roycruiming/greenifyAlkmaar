using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class raycaster : MonoBehaviour
{
    // Start is called before the first frame update

    public int rayLength;
    public LayerMask layerMask;
    public InventoryController inventoryController;


    private void Awake()
    {
        Inventory inv = new Inventory();
        inventoryController = new InventoryController(inv);
    }


    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //als de raycast een object raakt met de juiste layermask
        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {

            //en de speler drukt op f
            if (Input.GetKeyDown(KeyCode.F)) {

                //zoek naar de ItemHolder Component en haal hier het item uit
                Item itemToStore = hitInfo.collider.gameObject.GetComponent<Item>();

                //Transform t  = gameObject.transform.LookAt(;

               
                

                //itemToStore.transform.LookAt(this.transform);
                inventoryController.ReplaceWorldAndInventory(itemToStore, this.transform);

               
                
                
            }

        }

    }

   
}
    
