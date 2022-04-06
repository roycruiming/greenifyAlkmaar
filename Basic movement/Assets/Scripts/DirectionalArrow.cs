using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DirectionalArrow : MonoBehaviour
{
    public ObjectivesController objCon;


    [System.Obsolete]


    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();
    }

    private void Update()
    {
        //Arrow points to next objective
        if (objCon.targets.Count >= 1)
        {
            Vector3 targetPosition = objCon.targets[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);

        }
        else if(objCon.solarPanels.Count == 2 && objCon.solarPanelsSpot.Count == 2)
        {
            Vector3 targetPosition = objCon.solarPanels[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else if(objCon.solarPanels.Count == 1 && objCon.solarPanelsSpot.Count == 2)
        {
            Vector3 targetPosition = objCon.solarPanelsSpot[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else if (objCon.solarPanels.Count == 1 && objCon.solarPanelsSpot.Count == 1)
        {
            Vector3 targetPosition = objCon.solarPanels[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else if (objCon.solarPanels.Count == 0 && objCon.solarPanelsSpot.Count == 1)
        {
            Vector3 targetPosition = objCon.solarPanelsSpot[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }
}
