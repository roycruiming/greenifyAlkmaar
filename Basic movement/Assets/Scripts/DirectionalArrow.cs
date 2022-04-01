﻿using System.Collections;
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
    public GameObject blackBarArroundScoreScreen;
    public GameObject nameInput;
    public GameObject nameInputBar;
    public SubmitScore submitscore;
    public Text TextUiCounter;
    public Text GameTimer;
    public Text GameDone;
    public Text gameEndScore;
    public Text gameEndTime;
    public float secondsTimer;
    public int minutemark;
    public Button back;
    private int totalObjectives;
    public bool gameFinnished = false;

    public dreamloLeaderBoard dreamlo;

    [System.Obsolete]


    private void Awake()
    {
        GameDone.text = "";
        gameEndTime.text = "";
        gameEndScore.text = "";
        totalObjectives = target.Count;
        blackBarArroundScoreScreen.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        nameInputBar.gameObject.SetActive(false);

    }
    private void Update()
    {
        
        //Arrow points to next objective
        if (target.Count >= 5)
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
            blackBarArroundScoreScreen.gameObject.SetActive(true);
            nameInput.gameObject.SetActive(true);
            nameInputBar.gameObject.SetActive(true);
            GameDone.text = "Gefeliciteerd!";
            gameEndScore.text = objectivesCounter.ToString() + "/"+ totalObjectives;
            gameEndTime.text = "Tijd = " + minutemark + ":" + Mathf.Round(secondsTimer);
            back.gameObject.SetActive(true);
            gameFinnished = true;
            
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


    private int calculatescore()
    {
        int score = minutemark * 200;
        int score2 = Mathf.RoundToInt(secondsTimer) * score;
        return score2;
    }

    public void SubmitAndExit()
    {
        SubmitScore.AddNewHighscore(nameInput.GetComponent<Text>().text, 690/*calculatescore()*/);
        SceneManager.LoadScene(2);
    }

}
