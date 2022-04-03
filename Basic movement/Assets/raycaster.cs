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
    private InventoryController inventoryController;
    private Text textUI; 



    private void Awake()
    {
        Inventory inv = new Inventory();
        inventoryController = new InventoryController(inv);
    }


    async void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //als de raycast een object raakt met de juiste layermask
        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {

            //en de speler drukt op f
           if (Input.GetKeyDown(KeyCode.F)) {
                Item item = hitInfo.collider.gameObject.GetComponent<Item>();
                if (item == null) { return; }

                if (hitInfo.collider.gameObject.CompareTag("ObjectiveCube") && hitInfo.collider.transform.GetChild(1).gameObject.activeInHierarchy)
                {                    
                    hitInfo.collider.transform.GetChild(1).GetComponent<PuzzleDynaScript>().ActivatePuzzle();
                }

                else if (hitInfo.collider.gameObject.CompareTag("SolarSpot")) {
                    Item storedItem = inventoryController.GetItem(); 

                    if (item != null) {
                        hitInfo.collider.transform.GetComponent<SolarSpot>().DoShit(storedItem);
                  
                        this.inventoryController.ClearInventory(); 
                    }
                }
                else if (hitInfo.collider.gameObject.CompareTag("SolarPanel")) 
                {

                    //this.gameObject.GetComponent<InventoryScript>().AddOrSwap(hitInfo.collider);
                     

                    inventoryController.StoreItemAndPlacePreviouslyStoredItemInWorld(item, this.transform);

                    if(GameObject.FindWithTag("HUDCanvas") != null) {
                        //find the hudcontroller object and call the ShowcaseMessage a tutorial message
                        GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage("Good job you have found a Solarpanel! Find the spot where it should be placed.");
                    }
                }

                else if (hitInfo.collider.gameObject.CompareTag("InformationHelper"))
                {
                    //object is gamehelper so showcase this message in the HUD
                    //get the information text from the object and send it to the controller
                    if(hitInfo.collider.gameObject.GetComponent<InformationHelper>() != null && GameObject.FindWithTag("HUDCanvas") != null) {
                        //find the hudcontroller object and call the ShowcaseMessage function with the informationHelper message
                        InformationHelper senderInfo = hitInfo.collider.gameObject.GetComponent<InformationHelper>();
                        GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(senderInfo.informationText, senderInfo, new List<string> { "test test test", "vier vijf zes", "acht negen tien" });
                    }

                }
            }
        }
        else
        {
            if (textUI != null)
            {
                textUI.gameObject.SetActive(false);

            }
        }
    }  
}
    
