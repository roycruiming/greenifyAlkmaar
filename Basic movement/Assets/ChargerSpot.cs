using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSpot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int wtf = 0; 

  


    public bool InstallCharger(Item charger)
    {
        print("ffs"); 

        if (charger.unlockAbles.Contains(this.gameObject))
        {
            Vector3 pos = this.transform.position;
            Quaternion quat = transform.rotation;


            //print("lost");
            Vector3 p = this.transform.position;
            Quaternion q = transform.rotation;


            //Instantiate(GameObject.Find("collectable charger"), pos, quat);


            charger.GetComponent<AnimationScript>().enabled = false;
            Destroy(charger.GetComponent<Item>());
            charger.GetComponent<Outline>().enabled = false;
            GetComponent<Outline>().enabled = false;
            charger.transform.position = pos;
            charger.transform.rotation = quat; 
            charger.gameObject.SetActive(true);

            ObjectivesController oc = GameObject.Find("HUDCanvas").GetComponent<ObjectivesController>();

            oc.chargingStationCounter++; 


            if (oc.chargingStationCounter == 3) {

                print("counter reached"); 

                
                if (oc != null)
                {
                    oc.objectivesCounter++;
                } 
            }

            return true;
        }
        return false;
    }
}

