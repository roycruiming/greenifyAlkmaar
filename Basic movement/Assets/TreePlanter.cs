using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanter : MonoBehaviour
{
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 5, layerMask, QueryTriggerInteraction.Collide))
        {
            Hole hole = hitInfo.collider.gameObject.GetComponent<Hole>();
            if (hole != null && Input.GetKeyDown(KeyCode.F)) {

                InventoryController invc = this.gameObject.GetComponent<raycaster>().getInvController();

                Item tree = invc.GetItem();
                if (!hole.PlantTree(tree)) return;
                invc.ClearInventory();




            }

        }
    }
}
