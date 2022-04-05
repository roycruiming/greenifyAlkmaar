using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOpener : MonoBehaviour
{

    private bool IsStarted;
    

    public bool OpenDoors(Item item)
    {
        if (item == null) return false;
        if (!item.unlockAbles.Contains(this.gameObject)) return false;
        if (IsStarted) return false;   

        DoorOpener[] openers = GetComponentsInChildren<DoorOpener>();
       
        foreach (DoorOpener opener in openers) {
            StartCoroutine(opener.Open()); 
        }

        IsStarted = true;
        return true; 
        

    }


}
