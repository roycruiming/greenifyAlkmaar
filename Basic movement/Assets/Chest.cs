using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Chest : MonoBehaviour
{
    private bool isStarted = false;
   
    

    public bool OpenChest(Item item) {

        if (!item.unlockAbles.Contains(this.gameObject)) return false; 


    if (!isStarted) {

            //Destroy(lock1);
            //Destroy(lock2);
            LidOpener o = GetComponentInChildren<LidOpener>();
            isStarted = true;
            StartCoroutine(o.Rotate());
            
        }

        return true;












    }




}
