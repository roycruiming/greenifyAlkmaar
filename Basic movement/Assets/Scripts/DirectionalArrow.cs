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

    bool arrowValue;



    [System.Obsolete]
    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();
        this.arrowValue = GlobalGameHandler.PlayerWantsDirectionalArrow();

        HideOrShowArrow(arrowValue);
    }

    public void awake() {
        
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

        if(GlobalGameHandler.PlayerWantsDirectionalArrow() != arrowValue) {
            arrowValue = GlobalGameHandler.PlayerWantsDirectionalArrow();
            HideOrShowArrow(arrowValue);
        }
    }

    private void HideOrShowArrow(bool show) {
        if(show == false) this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 400, this.gameObject.transform.position.z);
        else this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 400, this.gameObject.transform.position.z);
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
