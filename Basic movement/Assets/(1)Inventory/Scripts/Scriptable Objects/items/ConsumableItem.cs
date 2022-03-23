using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "noname", menuName = "Inventory System/Items/Consumable")]
public class ConsumableItem : ItemObject
{
    public void Awake()
    {
        this.type = ItemType.Consumable;
    }
}
