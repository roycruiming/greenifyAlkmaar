using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "noname", menuName = "Inventory System/Items/Storeable")]
public class StoreableItem : ItemObject
{
    public void Awake()
    {
        type = ItemType.Storable; 
    }

}
