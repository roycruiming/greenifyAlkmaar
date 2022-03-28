using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;

    public InventoryController(Inventory _inventory) {
        this.inventory = _inventory; 
    }

    private void AddItem(Item item) {
        inventory.item = item;     
    }

    private Item GetItem() {
        return inventory.item; 

    }



}
