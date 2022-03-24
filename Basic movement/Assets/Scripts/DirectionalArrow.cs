using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalArrow : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> target;
    public int objectivesCounter = 0;
    public GameObject arrow;
    public Text textUi;

    [System.Obsolete]
    private void Update()
    {
        //Arrow points to next objective
        Vector3 targetPosition = target[0].transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        textUi.text = objectivesCounter.ToString() + "/5";

        //Destroy if all objects are done
        if (objectivesCounter == 5)
        {
            arrow.gameObject.active = false;
        }
    }

    public void DeleteItemInList(int valueTest)
    {
        target.RemoveAll(x => x.name =="Cube "+valueTest);
        objectivesCounter++;

    }


}
