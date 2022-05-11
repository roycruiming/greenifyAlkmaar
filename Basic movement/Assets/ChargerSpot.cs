using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSpot : MonoBehaviour
{

    private int counter = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal bool InstallCharger(Item charger)
    {
        print("ffs"); 

        if (charger.unlockAbles.Contains(this.gameObject))
        {
            Vector3 pos = this.transform.position;
            Quaternion quat = transform.rotation;
         

            print("lost");

            Instantiate(GameObject.Find("Electric-car-loader (2)"), pos, quat);


            charger.GetComponent<AnimationScript>().enabled = false;
            Destroy(charger.GetComponent<Item>());
            charger.GetComponent<Outline>().enabled = false;
            GetComponent<Outline>().enabled = false;
            charger.gameObject.SetActive(true);
            counter++;
            
            if (counter == 1) {
                ObjectivesController oc =  GameObject.Find("HUDCanvas").GetComponent<ObjectivesController>();

                if (oc != null) {
                    oc.objectivesCounter++;
                }
            
            }

            return true;
        }
        return false;
    }


}

