using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "inventory", menuName ="Inventory System/Inventory")]

public class IntventoryObject : ScriptableObject
{

    public int amountOfSlots; 
    public List<InventorySlot>  Container = new List<InventorySlot>();
    
    public bool AddItem(ItemObject _item, int _amount)
    {
        InventorySlot z = Container.FirstOrDefault(
            x => x.item == _item && x.amount < _item.permittedAmount);

        if (z != null)
        {
            z.AddAmount(_amount, _item.permittedAmount);
            return true;
        }
      
        if (Container.Count < this.amountOfSlots) {
            Container.Add(new InventorySlot(_item, _amount));
            return true; 
        }

        return false; 
        
  
    }
}


[System.Serializable]
public class InventorySlot {

    public ItemObject item;
    public int amount; 
    public InventorySlot(ItemObject _item, int _amount)
    {
        this.item = _item;
        this.amount = _amount; 

    }

    public void AddAmount(int value, int? permittedAmount = null)
    {
        //stack till max
        if (permittedAmount != null && amount + value >= permittedAmount.Value)
        {
            amount = permittedAmount.Value;
        }

        //stack
        else {
            amount += value;         
        }
    }
}
