using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory Inventory;

    public InventoryController(Inventory inventory) {
        this.Inventory = inventory; 
    }

    public void StoreItemAndPlacePreviouslyStoredItemInWorld(Item item, Transform transformToLookAt)
    {
        Item previouslyStoredItem = StoreItemAndReturnPreviouslyStoredItemOrNull(item);      
        UpdateWorld(item, previouslyStoredItem, transformToLookAt);
    }

    
    //stores item, returns the previously stored item or null when inventory empty
    private Item StoreItemAndReturnPreviouslyStoredItemOrNull(Item itemToStore)
    {
        Item toReturn = Inventory.item;
        Inventory.item = itemToStore;
        return toReturn;
    }

   //Sets stored item inactive and places previously stored item in the world
    private void UpdateWorld(Item  storedItem, Item previouslyStoredItem, Transform transformToLookAt)
    {  
        //item that has been stored is set inactive. 
        storedItem.gameObject.SetActive(false);
        //if there is no stored item in te inventory return
        if (previouslyStoredItem == null) { return ; }
        //applies given heighOffsetY
        Vector3 pos = storedItem.transform.position;
        pos.y = pos.y - storedItem.HeightOffsetY + previouslyStoredItem.HeightOffsetY;
        //items replace position
        previouslyStoredItem.transform.position = pos; 
        //item faces transform
        previouslyStoredItem.transform.LookAt(transformToLookAt);
        //lock lookat on the y-axis
        Vector3 angles = previouslyStoredItem.transform.eulerAngles;
        Vector3 onlyYaxisAngles = new Vector3(0f, angles.y + previouslyStoredItem.RotationOffsetY, 0f);
        previouslyStoredItem.gameObject.transform.eulerAngles = onlyYaxisAngles;
        //set the game object active
        previouslyStoredItem.gameObject.SetActive(true); 
    }

    public Item GetItem() {
        return Inventory.item; 
    }

    public void ClearInventory() {
        Inventory.item = null;
        GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().RemoveImage(); 
    }



}
