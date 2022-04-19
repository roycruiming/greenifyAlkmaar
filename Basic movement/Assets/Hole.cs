using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update


    internal bool PlantTree(Item tree)
    {
        if (tree.unlockAbles[0].CompareTag("emptyCollider"))
            
        {
 

            Vector3 n = this.gameObject.transform.position;
            n.y = tree.gameObject.transform.position.y - 1.3f;
            
            GameObject qh = tree.gameObject;
            qh.SetActive(true); 


            Instantiate(qh, n, Quaternion.identity);
            GameObject.Find("Terrain").GetComponent<LowerTerrainOnImpact>().SetTerrainOk();
            
            return true; 
        }
        else {
            return false;        
        }
        //throw new NotImplementedException();
    }
}
