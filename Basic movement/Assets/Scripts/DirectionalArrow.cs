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
    public Text TextUiCounter;
    public Text GameTimer;
    private float secondsTimer;
    private int minutemark;


    [System.Obsolete]
    private void Update()
    {
        //Arrow points to next objective
        Vector3 targetPosition = target[0].transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        TextUiCounter.text = objectivesCounter.ToString() + "/5";

        //Destroy if all objects are done
        if (objectivesCounter == 5)
        {
            //arrow.gameObject.active = false;
            arrow.transform.position = new Vector3(100, 100, 100);
        }


        //timer in game.
        secondsTimer += Time.deltaTime;
        if(secondsTimer > 59.45)
        {
            secondsTimer = 0;
            minutemark++;
        }
        GameTimer.text = minutemark + ":" + Mathf.Round(secondsTimer);
    }


    //delete item in list for objectives. 
    public void DeleteItemInList(int valueTest)
    {
        target.RemoveAll(x => x.name =="Cube "+valueTest);
        objectivesCounter++;

    }


}
