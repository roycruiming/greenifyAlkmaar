using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;

    public InventoryController(Inventory _inventory) {
        this.inventory = _inventory; 
    }


    public void ReplaceWorldAndInventory(Item itemToStore)
    {
        Item previouslyStoredItem = AddItemToInventoryAndReturnPreviousOrNull(itemToStore);

        if (previouslyStoredItem != null)
        {
            ReplaceGameObjects(itemToStore.GetGameObject(), previouslyStoredItem.GetGameObject());
        }

        else {
            ReplaceGameObjects(itemToStore.GetGameObject(), null); 
        }


        
    }

    private void ReplaceGameObjects(GameObject replacedItem, GameObject replacementItem)
    {
        replacedItem.SetActive(false);

        if (replacementItem != null)
        {
            Vector3 replacedPosition = replacedItem.transform.position;
            UnityEngine.Quaternion replacedQuaternion = replacedItem.transform.rotation;
            replacementItem.transform.position = replacedPosition;
            replacementItem.transform.rotation = replacedQuaternion;
            replacementItem.SetActive(true);
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
