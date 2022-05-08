using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treespot : MonoBehaviour
{
    public bool PlantTree(Item item) {

        if (item.unlockAbles.Contains(this.gameObject))
        {
            Transform burrier = transform.parent.Find("burrier");
            burrier.gameObject.SetActive(true);

            item.GetComponent<AnimationScript>().enabled = false;
            Destroy(item.GetComponent<Item>());
            
     
            item.GetComponent<Outline>().enabled = false; 
            item.transform.position = burrier.transform.position;
            item.gameObject.SetActive(true);
            
            GlobalGameHandler.GivePlayerCoints(Random.Range(1, 3)); 
            return true;
            
        }



        return false; 

    }


}
