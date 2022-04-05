using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ObjectivesController : MonoBehaviour
{

    public List<PuzzleController> targets;
    public int objectivesCounter = 0;
    private int totalObjectives;

    public float secondsTimer;
    public int minutemark;
    public Text GameTimer;

    public Text TextUiCounter;
    public Text GameDone;
    public Text gameEndScore;
    public Text gameEndTime;
    public GameObject blackBarArroundScoreScreen;
    public GameObject nameInput;
    public GameObject nameInputBar;

    public Button back;

    public bool gameFinnished = false;

    public void Awake()
    {
        targets = Resources.FindObjectsOfTypeAll<PuzzleController>().ToList();
        GameDone.text = "";
        gameEndTime.text = "";
        gameEndScore.text = "";
        totalObjectives = targets.Count;
        blackBarArroundScoreScreen.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        nameInputBar.gameObject.SetActive(false);
        back.gameObject.SetActive(false);



    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timer in game.
        secondsTimer += Time.deltaTime;
        if (secondsTimer > 59.45)
        {
            secondsTimer = 0;
            minutemark++;
        }
        GameTimer.text = minutemark + ":" + Mathf.Round(secondsTimer);

        // Set how mutch objectives are done
        TextUiCounter.text = objectivesCounter.ToString() + "/5";


        //when objectivesList == emtpy - game is finnished
        if(targets.Count == 4)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            blackBarArroundScoreScreen.gameObject.SetActive(true);
            nameInput.gameObject.SetActive(true);
            nameInputBar.gameObject.SetActive(true);
            GameDone.text = "Gefeliciteerd!";
            gameEndScore.text = objectivesCounter.ToString() + "/" + totalObjectives;
            gameEndTime.text = "Tijd = " + minutemark + ":" + Mathf.Round(secondsTimer);
            back.gameObject.SetActive(true);
            gameFinnished = true;
        }
    }

    public void DeleteItemInList(PuzzleController resolvedTask)
    {
        targets.Remove(resolvedTask);
        objectivesCounter++;

    }

    public void SetTimerToNul()
    {
        secondsTimer = 0;
        minutemark = 0;
    }


    public void SubmitAndExit()
    {
        int secondsFinal = (int)Mathf.Round(secondsTimer);
        int result = minutemark * 100;
        SubmitScore.AddNewHighscore(nameInput.GetComponent<Text>().text, result + secondsFinal);

        SceneManager.LoadScene(2);
    }
}
