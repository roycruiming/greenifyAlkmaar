using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DirectionalArrow : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> target;
    public int objectivesCounter = 0;
    public GameObject arrow;
    public Text TextUiCounter;
    public Text GameTimer;
    public Text GameDone;
    public Text gameEndScore;
    public Text gameEndTime;
    public float secondsTimer;
    public int minutemark;
    public Button back;
    private int totalObjectives;


    [System.Obsolete]


    private void Awake()
    {
        GameDone.text = "";
        gameEndTime.text = "";
        gameEndScore.text = "";
        totalObjectives = target.Count;
    }
    private void Update()
    {
        //Arrow points to next objective
        if (target.Count >= 1)
        {
            Vector3 targetPosition = target[0].transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);   
            back.gameObject.SetActive(false);
        }
        //when game is finnished
        else
        {
            Cursor.lockState = CursorLockMode.None;
            arrow.transform.position = new Vector3(100, 100, 100);
            Time.timeScale = 0;
            GameDone.text = "Gefeliciteerd!";
            gameEndScore.text = objectivesCounter.ToString() + "/"+ totalObjectives;
            gameEndTime.text = "Tijd = " + minutemark + ":" + Mathf.Round(secondsTimer);
            back.gameObject.SetActive(true);
            
        }


        TextUiCounter.text = objectivesCounter.ToString() + "/5";



        //timer in game.
        secondsTimer += Time.deltaTime;
        if (secondsTimer > 59.45)
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

    public void SetTimerToNul()
    {
        secondsTimer = 0;
        minutemark = 0;
    }

/*    public void StartTimer()
    {

    }*/


}
