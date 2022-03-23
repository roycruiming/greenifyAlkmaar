using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSpot : MonoBehaviour
{
    // Start is called before the first frame update

    public void DoShit(InventorySlot infslot) {

        if (this.gameObject.name == "PlaceHerePanelSpot1" && infslot.item.name == "SOLAR") {
            GameObject obj = GameObject.Find("Cube (1)");
            GameObject child = obj.transform.Find("Smoke").gameObject;
            child.SetActive(false); 
        }

        print("im doing shit"); 
    }

    private void OnTriggerEnter(Collider other)
    {        
    }
}
