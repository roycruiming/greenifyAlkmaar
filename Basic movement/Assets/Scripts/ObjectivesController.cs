using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Linq;

public class ObjectivesController : MonoBehaviour
{

    public List<PuzzleController> targets;
    public List<Item> solarPanels;
    public List<GameObject> solarPanelsSpot;
    public int objectivesCounter = 0;
    private int totalObjectives;

    private bool testPhaseBooleanVerticalSlice = false; //remove in later stage!

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
    public string levelName = "De Meent";
    public bool analyticsSend = false;
    public Canvas puzzleCanvas;

    public void Awake()
    {
        TextUiCounter = GameObject.Find("ObjectivesCounter").GetComponent<Text>();
        GameTimer = GameObject.Find("GameTimer").GetComponent<Text>();
        GameDone = GameObject.Find("EndScreen").GetComponent<Text>();
        gameEndScore = GameObject.Find("EndTime").GetComponent<Text>();
        gameEndTime = GameObject.Find("EndTime").GetComponent<Text>();
        nameInput = GameObject.Find("InputFieldName");
        nameInputBar = GameObject.Find("EndScore");
        blackBarArroundScoreScreen = GameObject.Find("BlackBar");
        back = GameObject.Find("ExitLevelButton").GetComponent<Button>();




        targets = Resources.FindObjectsOfTypeAll<PuzzleController>().ToList();
        solarPanels = Resources.FindObjectsOfTypeAll<Item>().ToList();
        solarPanelsSpot = GameObject.FindGameObjectsWithTag("SolarSpot").ToList();
        GameDone.text = "";
        gameEndTime.text = "";
        gameEndScore.text = "";
        blackBarArroundScoreScreen.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        nameInputBar.gameObject.SetActive(false);
        back.gameObject.SetActive(false);

        totalObjectives = targets.Count + solarPanels.Count;


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        for (var i = solarPanelsSpot.Count - 1; i > -1; i--)
        {
            if (solarPanelsSpot[i] == null)
            {
                solarPanelsSpot.RemoveAt(i);
                objectivesCounter++;
            }
        }

        //timer in game.
        secondsTimer += Time.deltaTime;
        if (secondsTimer > 59.45)
        {
            secondsTimer = 0;
            minutemark++;
        }
        GameTimer.text = minutemark.ToString("00") + ":" + Mathf.Round(secondsTimer).ToString("00");

        // Set how many objectives are done
        TextUiCounter.text = objectivesCounter + "/" + totalObjectives;


        //when objectivesList == emtpy - game is finished
       /* if(targets.Count == 0 && solarPanels.Count == 0 && solarPanelsSpot.Count == 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
            puzzleCanvas.gameObject.SetActive(false);

            blackBarArroundScoreScreen.gameObject.SetActive(true);
            nameInput.gameObject.SetActive(true);
            nameInputBar.gameObject.SetActive(true);
            GameDone.text = "Gefeliciteerd!";
            gameEndScore.text = objectivesCounter.ToString() + "/" + totalObjectives;
            gameEndTime.text = "Tijd = " + minutemark + ":" + Mathf.Round(secondsTimer);
            back.gameObject.SetActive(true);
            gameFinnished = true;

            if(!analyticsSend){
              AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "LevelWin ",
                new Dictionary<string, object> {
                  {"level", levelName},
                  {"time", minutemark + ":" + Mathf.Round(secondsTimer)}
                  });
              print(analyticsResult);
              analyticsSend = true;
            }
        }*/
    }

    public void DeleteItemInList(PuzzleController resolvedTask)
    {
        targets.Remove(resolvedTask);
        objectivesCounter++;
        //check if half of the tasks have been completed if so showcase the level progression
        if(objectivesCounter >= (this.totalObjectives/2) && testPhaseBooleanVerticalSlice == false) {
            testPhaseBooleanVerticalSlice = true;
            GameObject.Find("LevelObject").GetComponent<MeentLevel>().showcaseLevelProgression();
        }
    }

    public void DeleteItemInListSolar(Item solarpanels)
    {

        solarPanels.Remove(solarpanels);
        //objectivesCounter++;

    }

    public void DeleteItemInListSolarSpot(GameObject solarpanelsSpot)
    {
        solarPanelsSpot.Remove(solarpanelsSpot);
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
        SubmitScore.AddNewHighscore(nameInput.GetComponent<InputField>().text, result + secondsFinal);

        SceneManager.LoadScene(3);
    }
}
