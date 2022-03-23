using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class raycaster : MonoBehaviour
{
    // Start is called before the first frame update

    public int rayLength;
    public LayerMask layerMask;
    public Text textUI; 

     


    void Start()
    {
        if (textUI != null) {
            textUI.text = ""; 
            textUI.gameObject.SetActive(false); 
        }
    }


    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {
      
            OnScreenDescription description
                = hitInfo.collider.gameObject.GetComponent<OnScreenDescription>();

            if (textUI != null && description != null) {
                textUI.gameObject.SetActive(true);
                textUI.text = description.textToDisplay; 
            }

            if (Input.GetKeyDown(KeyCode.F)) {

                if (hitInfo.collider.gameObject.CompareTag("ObjectiveCube"))
                {
                    hitInfo.collider.transform.GetChild(1).GetComponent<PuzzleDynaScript>().ActivatePuzzle();
                }
                else if (hitInfo.collider.gameObject.CompareTag("SolarSpot")) {
                    InventorySlot infslot = gameObject.GetComponent<InventoryScript>().inventory.Container.FirstOrDefault(); 
                    if (infslot != null) {
                        hitInfo.collider.transform.GetComponent<SolarSpot>().DoShit(infslot);
                    }
                }
                else
                {
                    Destroy(hitInfo.collider.gameObject);
                    this.gameObject.GetComponent<InventoryScript>().doshit(hitInfo.collider);
                }
            }
        }
        else
        {

            print("wtf"); 

            if (textUI != null)
            {
                textUI.gameObject.SetActive(false);

            }
        }

    }


}
