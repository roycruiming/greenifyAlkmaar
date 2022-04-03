using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void printshit(string shit);

public class SolarSpot : MonoBehaviour
{
    //
    //    Start is called before the first frame update

    public void DoShit(Item item)
    {



        if (this.gameObject.name == "PlaceHerePanelSpot1" && item.gameObject.tag == "SolarPanel")
        {
            ActivateBox(GameObject.Find("Cube 2"), item);
        }

        else if (this.gameObject.name == "PlaceHerePanelSpot2" && item.gameObject.tag == "SolarPanel")
        {
            ActivateBox(GameObject.Find("Cube 1"), item);
        }

    }


    private void ActivateBox(GameObject obj, Item item)
    {
        GameObject child = obj.transform.Find("Smoke").gameObject;
        child.SetActive(false);

        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.transform.GetChild(2).gameObject.SetActive(false);

        Vector3 pos = gameObject.transform.position;
        Quaternion quat = gameObject.transform.rotation;
        GameObject f = item.gameObject;
        Destroy(gameObject);
        f.transform.position = pos;
        f.transform.rotation = quat;
        f.transform.position = new Vector3(f.transform.position.x, f.transform.position.y - 4, f.transform.position.z);
        f.GetComponent<BoxCollider>().enabled = false;
        f.SetActive(true);

    }
}






