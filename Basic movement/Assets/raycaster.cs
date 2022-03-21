using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class raycaster : MonoBehaviour
{
    // Start is called before the first frame update

    public int rayLength;
    public LayerMask layerMask;
    public Text textUI;
     


    void Start()
    {
        textUI.text = ""; 
        textUI.gameObject.SetActive(false); 

    }

    // Update is called once per frame
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
                Destroy(hitInfo.collider.gameObject);  
                this.gameObject.GetComponent<InventoryScript>().doshit(hitInfo.collider);
           
            }
            
            


            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            


        }
        else
        {
            textUI.gameObject.SetActive(false); 

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
        }

    }


}
