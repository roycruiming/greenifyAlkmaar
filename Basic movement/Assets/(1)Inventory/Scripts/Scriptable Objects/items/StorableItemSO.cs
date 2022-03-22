using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "noname", menuName = "Inventory System/Items/Storeable")]
public class StorableItemObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Storable; 
    }

}
