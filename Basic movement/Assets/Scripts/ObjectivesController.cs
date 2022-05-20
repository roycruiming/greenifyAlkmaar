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
    public int objectivesCounter ;
    private int totalObjectives;

    private bool testPhaseBooleanVerticalSlice = false; //remove in later stage!

    public float secondsTimer;
    public int minutemark;
    public Text GameTimer;

    public Text TextUiCounter;
    public Text GameDone;
    public Text gameEndScore;
    public Text gameEndTime;
    public Text AmmountCoins;
    public GameObject blackBarArroundScoreScreen;
    public GameObject nameInput;
    public GameObject nameInputBar;
    public GameObject pauseMenu;
    public GameObject puzzleCanvass;
    public GameObject AmmountOfCoins;
    public GameObject objectivesObject;
    public GameObject GameTimerObject;

    public int chargingStationCounter = 0;

    public Button back;

    public bool gameFinnished = false;
    public string levelName;
    public bool analyticsSend = false;
    public Canvas puzzleCanvas;

    private bool progression1PhaseDone = false;
    private bool progression2PhaseDone = false;

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
        AmmountCoins = GameObject.Find("MoneyAmount").GetComponent<Text>();
        AmmountOfCoins = GameObject.Find("MoneyAmount");
        pauseMenu = GameObject.Find("PauseMenu 1");
        puzzleCanvass = GameObject.Find("PuzzleCanvas");
        objectivesObject = GameObject.Find("ObjectivesCounter");
        GameTimerObject = GameObject.Find("GameTimer");

        AmmountCoins.text = "0";

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

        if (GameObject.Find("MeentV2") != null)
        {
            solarPanels.RemoveAt(0);
            solarPanels.RemoveAt(0);
            solarPanels.RemoveAt(1);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].name == "ObjectiveBox")
                {
                    targets.RemoveAt(i);
                }
            }
        }





        if (GameObject.Find("Kaasmarkt scenery") != null)
        {
            totalObjectives = targets.Count + 1;
        }
        else
        {
            totalObjectives = targets.Count + solarPanels.Count;

        }

        if (GameObject.Find("AzStadion") != null)
        {
            AzStadion();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //objectivesCounter = totalObjectives; 
        //RedirectToPhotoScoreScreen(); 
       // StartCoroutine(secondsTest());
       // solarPanels.Clear();
       // targets.Clear();

    }

    private IEnumerator secondsTest() {


        yield return new WaitForSeconds(3f);
        objectivesCounter++; 
    
    
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

        if (GameObject.Find("AzStadion") == null)
        {
            AmmountCoins.text = GlobalGameHandler.GetTotalPlayerCointsAmount().ToString();




            this.CheckNextProgressionPhase();
            //when objectivesList == emtpy - game is finished
            if (targets.Count == 0 && solarPanels.Count == 0 && objectivesCounter == totalObjectives)
            {


                RedirectToPhotoScoreScreen(); 
                return; 

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(false);
                puzzleCanvass.SetActive(false);
                blackBarArroundScoreScreen.gameObject.SetActive(true);
                if (GameObject.Find("TutorialLevel"))
                {

                }
                else
                {
                    nameInputBar.gameObject.SetActive(true);
                }

                GameDone.text = "Gefeliciteerd!";
                gameEndScore.text = objectivesCounter.ToString() + "/" + totalObjectives;
                gameEndTime.text = "Tijd = " + minutemark + ":" + Mathf.Round(secondsTimer);
                back.gameObject.SetActive(true);
                gameFinnished = true;

                if (!analyticsSend)
                {
                    if (GameObject.Find("LevelHandler").GetComponent<MeentLevel>())
                    {
                        levelName = GameObject.Find("LevelHandler").GetComponent<MeentLevel>().levelName;
                    }
                    else if (GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>())
                    {
                        levelName = GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>().levelName;
                    }
                    else if (GameObject.Find("TutorialLevelHandler").GetComponent<TutorialLevel>())
                    {
                        levelName = GameObject.Find("TutorialLevelHandler").GetComponent<TutorialLevel>().levelName;
                    }

                    AnalyticsResult analyticsResult = Analytics.CustomEvent(
                      "LevelWin ",
                      new Dictionary<string, object> {
                   {"level", levelName},
                   {"time", minutemark + ":" + Mathf.Round(secondsTimer)}
                        });
                    analyticsSend = true;
                }
            }
        }
    }

    private void RedirectToPhotoScoreScreen()
    {
        string score = GameObject.Find("GameTimer").GetComponent<Text>().text;
        GameObject OnSceneLoaded = GameObject.Find("OnSceneLoaded");

        OnSceneLoaded.GetComponent<SceneLoaded>().Time = score; 

        Object.DontDestroyOnLoad(GameObject.Find("3RD Person"));
        Object.DontDestroyOnLoad(GameObject.Find("OnSceneLoaded"));
        SceneManager.LoadScene(18);

        
       //loader1.onSceneLoaded += MySceneLoadHandler;

        //throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        string str = UnityEngine.StackTraceUtility.ExtractStackTrace();
    }

    private void CheckNextProgressionPhase()
    {
        if (objectivesCounter == 2 && progression1PhaseDone == false)
        {
            progression1PhaseDone = true;
            if (GameObject.Find("LevelHandler").GetComponent<MeentLevel>() != null) GameObject.Find("LevelHandler").GetComponent<MeentLevel>().showcaseLevelProgression();
            else if (GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>() != null) GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>().showcaseLevelProgression();
        }
        else if (objectivesCounter == 4 && progression2PhaseDone == false)
        {
            progression2PhaseDone = true;
            if (GameObject.Find("LevelHandler").GetComponent<MeentLevel>() != null) GameObject.Find("LevelHandler").GetComponent<MeentLevel>().showcaseLevelProgression();
            else if (GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>() != null) GameObject.Find("LevelHandler").GetComponent<KaasmarktLevel>().showcaseLevelProgression();
        }
    }

    public void DeleteItemInList(PuzzleController resolvedTask)
    {
        targets.Remove(resolvedTask);
        objectivesCounter++;
        //check if half of the tasks have been completed if so showcase the level progression
        CheckNextProgressionPhase();

        //old code
        // if(objectivesCounter >= (this.totalObjectives/2) && testPhaseBooleanVerticalSlice == false) {
        //     testPhaseBooleanVerticalSlice = true;
        //     GameObject.Find("LevelObject").GetComponent<MeentLevel>().showcaseLevelProgression();
        // }
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
        if (GameObject.Find("TutorialLevel") != null)
        {
            print("testtttt");
        }

        if (GameObject.Find("MeentV2") != null)
        {
            print("testtttt");
            /*int secondsFinal = (int)Mathf.Round(secondsTimer);
            int result = minutemark * 100;
            SubmitScore.AddNewHighscore(nameInput.GetComponent<InputField>().text, result + secondsFinal);*/
        }



        SceneManager.LoadScene("Mainmap-Scene");
    }

    public void AzStadion()
    {
        AmmountOfCoins.SetActive(false);
        objectivesObject.SetActive(false);
        GameTimerObject.SetActive(false);
    }
}
