using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DirectionalArrow : MonoBehaviour
{
    public ObjectivesController objCon;

    private float oldDistance = 9999;
    PuzzleController closetsObject;



    [System.Obsolete]


    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();

        
    }

    private void Update()
    {


        if (objCon != null && objCon.targets.Count > 0)
        {
            //Vector3 targetPosition = GetClosestObjective(objCon.targets).transform.position;
            Vector3 targetPosition = objCon.targets[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


/*    PuzzleController GetClosestObjective(List<PuzzleController> objects)
    {

        foreach (PuzzleController g in objects)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closetsObject = g;
                oldDistance = dist;
            }
        }
        return closetsObject;
    }*/
}
