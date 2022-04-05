using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SolarSpot : MonoBehaviour
{
    //
    //    Start is called before the first frame update

    public GameObject Box;

    public void DoShit(Item item)
    {

        if (item.gameObject.tag == "SolarPanel") {

            ActivateBox(item); 
        }

    }


    private void ActivateBox( Item item)
    {
        Vector3 pos = gameObject.transform.position;
        Quaternion quat = gameObject.transform.rotation;

        GameObject itemGameObject = item.gameObject;
        Destroy(gameObject);
        itemGameObject.transform.position = pos;
        itemGameObject.transform.rotation = quat;
        itemGameObject.transform.position = new Vector3(itemGameObject.transform.position.x, itemGameObject.transform.position.y - 4, itemGameObject.transform.position.z);
        itemGameObject.GetComponent<BoxCollider>().enabled = false;
        itemGameObject.SetActive(true);

        //Box.Find("Smoke").gameObject.SetActive(false);

        Box.transform.Find("Smoke").gameObject.SetActive(false);  
        Box.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        //objectivesController.DeleteItemInList(this);
    }
}






