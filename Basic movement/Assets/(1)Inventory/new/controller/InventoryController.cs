using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;

    public InventoryController(Inventory _inventory) {
        this.inventory = _inventory; 
    }


    public void ReplaceWorldAndInventory(Item itemToStore, Transform lookAt)
    {
        //TODO: CLEAN UP THIS CODE

        



        Item previouslyStoredItem = AddItemToInventoryAndReturnPreviousOrNull(itemToStore);
       
        //float x = itemToStore.heightOffsetY - previouslyStoredItem.heightOffsetY;



        if (previouslyStoredItem != null)
        {
            ReplaceGameObjects(itemToStore, previouslyStoredItem, lookAt);
        }
        else {
            ReplaceGameObjects(itemToStore, null, lookAt); 
        }


        
    }

    private void ReplaceGameObjects(Item  itemToReplace, Item replacementItem, Transform lookat)
    {
        itemToReplace.gameObject.SetActive(false);

        if (replacementItem != null)
        {



            //positioning 
             replacementItem.transform.position = itemToReplace.transform.position;
             Vector3 pos = replacementItem.transform.position;
             pos.y = replacementItem.getHeightOffsetY();
             replacementItem.transform.position = pos; 
 
            //AngleRotation
            replacementItem.transform.LookAt(lookat);
            Vector3 angles = replacementItem.transform.eulerAngles;
            Vector3 onlyYaxisAngles = new Vector3(0f, angles.y + replacementItem.rotationOffsetY, 0f); ;
            replacementItem.gameObject.transform.eulerAngles = onlyYaxisAngles;
            replacementItem.gameObject.SetActive(true);
        }
    }


    // Adds an item, returns previous item, 
    public Item AddItemToInventoryAndReturnPreviousOrNull(Item _item)
    {
        Item toReturn = inventory.item;
        inventory.item = _item;
        return toReturn;
    }

    public Item GetItem() {
        return inventory.item; 
    }

    public void ClearInventory() {
        inventory.item = null;     
    }



}
