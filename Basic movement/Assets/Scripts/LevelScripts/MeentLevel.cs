using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeentLevel : MonoBehaviour, LevelBasis
{
    public string levelName { get; set; }
    public int totalTasksCount { get; set; }
    public int completedTasksCount { get; set; }
    public bool hasPlayedBefore { get; set; }

    public int progressionPhase { get; set; }

    public List<GameObject> allPhaseObjects { get; set; }

    private List<List<GameObject>> allPhaseObjectsList = new List<List<GameObject>>();

    private bool blinkProgressObjects;
    private int objectBlinkCounter;

    float elapsedTime = 0f;

    public void Awake() {
        initLevel();
    }

    public void initLevel()
    {
        //if no progress exists in save file:
        this.levelName = GlobalGameHandler.GetTextByDictionaryKey("the meent");

        totalTasksCount = 5;

        completedTasksCount = 0;

        hasPlayedBefore = false;

        progressionPhase = -1;

        objectBlinkCounter = 0;

        blinkProgressObjects = false;

        //else load save files (implement later)



        //get all phase objects
        //save all phase 1 (index = 0) objects and hide them
        this.allPhaseObjectsList.Add(GameObject.FindGameObjectsWithTag("ProgressPhase1Object").ToList<GameObject>());
        foreach(GameObject g in this.allPhaseObjectsList[0]) g.SetActive(false);
        
    }

    public void taskCompleted() {
        if(completedTasksCount > totalTasksCount/2) showcaseLevelProgression();
    }

    public void showcaseLevelProgression() {
        progressionPhase++; //up the progression level phase

        if(allPhaseObjectsList[progressionPhase] != null) {
            allPhaseObjects = allPhaseObjectsList[progressionPhase];
            this.blinkProgressObjects = true;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 0.6f) {
            elapsedTime = elapsedTime % 0.6f; //every 0.6 seconds
            
            if(blinkProgressObjects) blinkPhaseObjects();
        }
    }

    private void blinkPhaseObjects() {
        this.objectBlinkCounter++;

        if(this.allPhaseObjects != null && this.allPhaseObjects.Count > 0) {
            setActiveStateObjects(!allPhaseObjects[0].activeSelf); //toggle active self 'blink effect'
        }
        
        if(this.objectBlinkCounter == 18) {
            this.blinkProgressObjects = false;
            objectBlinkCounter = 0;
            setActiveStateObjects(true);
        }
    }

    private void setActiveStateObjects(bool state) {
        if(allPhaseObjects != null) foreach(GameObject g in allPhaseObjects) g.SetActive(state);
    }
}
