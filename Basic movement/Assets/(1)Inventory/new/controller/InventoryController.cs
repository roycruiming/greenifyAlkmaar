using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;

    public InventoryController(Inventory _inventory) {
        this.inventory = _inventory; 
    }

    // Adds an item, returns previous item, 
    public Item AddItemAndReturnPreviousOrNull(Item _item) {
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
