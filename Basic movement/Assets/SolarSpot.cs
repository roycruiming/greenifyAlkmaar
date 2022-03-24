using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSpot : MonoBehaviour
{
    // Start is called before the first frame update

    public void DoShit(InventorySlot infslot) {



        if (this.gameObject.name == "PlaceHerePanelSpot1" && infslot.item.name == "SOLAR")
        {
            ActivateBox(GameObject.Find("Cube 2"), infslot);
        }

        else if (this.gameObject.name == "PlaceHerePanelSpot2" && infslot.item.name == "SOLAR")
        {
            ActivateBox(GameObject.Find("Cube 1"), infslot);
        }

    }


    private void ActivateBox(GameObject obj, InventorySlot infslot) {
        GameObject child = obj.transform.Find("Smoke").gameObject;
        child.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.transform.GetChild(2).gameObject.SetActive(false);

        Vector3 pos = gameObject.transform.position;
        Quaternion quat = gameObject.transform.rotation;
        GameObject f = infslot.item.getprefab();
        Destroy(gameObject);
        f.transform.position = pos;
        f.transform.rotation = quat;
        f.transform.position = new Vector3(f.transform.position.x, f.transform.position.y - 4, f.transform.position.z);
        f.GetComponent<BoxCollider>().enabled = false;
        f.SetActive(true);

    }

       // print("im doing shit"); 
 }




