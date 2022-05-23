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

    private GameObject cutsceneParent;
    private GameObject mainCamera;

    private bool blinkProgressObjects;
    private int objectBlinkCounter;
    private int amountOfBlinks = 18;

    float elapsedTime = 0f;

    public void Awake() {
        
    }

    public void Start() {
        initLevel();

        //showcase intro cinematic
        StartCoroutine(showcaseIntroCutscene());
        print(progressionPhase + " start");
        
    }

    IEnumerator showcaseIntroCutscene() {
        
        this.mainCamera.SetActive(false);
        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("intro de meent"));
        yield return new WaitForSeconds(18);

        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(false);
        this.mainCamera.SetActive(true);
        yield return null;

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

        this.cutsceneParent = GameObject.Find("cutscenesHolder");

        this.mainCamera = GameObject.Find("Main Camera");

        //else load save files (implement later)



        //get all phase objects
        //save all phase 1 (index = 0) objects and hide them
        this.allPhaseObjectsList.Add(GameObject.FindGameObjectsWithTag("ProgressPhase1Object").ToList<GameObject>());
        this.allPhaseObjectsList.Add(GameObject.FindGameObjectsWithTag("ProgressPhase2Object").ToList<GameObject>());
        foreach(GameObject g in this.allPhaseObjectsList[0]) g.SetActive(false);
        foreach(GameObject g in this.allPhaseObjectsList[1]) g.SetActive(false);
        
    }



    public void saveProgress() {

    }

    public void taskCompleted() {
        if(completedTasksCount > totalTasksCount/2) showcaseLevelProgression();
    }

    public void showcaseLevelProgression() {
        progressionPhase++; //up the progression level phase
        print("progressionphase=" + progressionPhase);
        if(allPhaseObjectsList[progressionPhase] != null) {
            //initiate the blinking of the new greener level-props
            allPhaseObjects = allPhaseObjectsList[progressionPhase];
            this.blinkProgressObjects = true;
            
            //add rewards if needed=

            if(progressionPhase == 0) {
                SwitchCamera(this.cutsceneParent.transform.Find("Progression1Phase").gameObject,this.mainCamera);
                GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("the meent text phase 1"));
                //reward the player
                if(GameObject.Find("HUDCanvas").GetComponent<HUDController>() != null) {
                    GameObject.Find("HUDCanvas").GetComponent<HUDController>().showcaseAndUnlockUnlockable(1);
                    GameObject.Find("HUDCanvas").GetComponent<HUDController>().showcaseAndUnlockUnlockable(2);
                }
                GlobalGameHandler.GivePlayerCoints(Random.Range(801,870));
            }
            else if(progressionPhase == 1) {
                SwitchCamera(this.cutsceneParent.transform.Find("Progression2Phase").gameObject,this.mainCamera);
                GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("the meent text phase 2"));

                //reward the player
                if(GameObject.Find("HUDCanvas").GetComponent<HUDController>() != null) GameObject.Find("HUDCanvas").GetComponent<HUDController>().showcaseAndUnlockUnlockable(3);
                GlobalGameHandler.GivePlayerCoints(Random.Range(801,870));
            }
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
            setActiveStateObjects(!allPhaseObjects[progressionPhase].activeSelf); //toggle active self 'blink effect'
        }
        
        if(this.objectBlinkCounter == this.amountOfBlinks) {
            this.blinkProgressObjects = false;
            objectBlinkCounter = 0;
            setActiveStateObjects(true);

            //switch camera back
            if(progressionPhase == 0) this.SwitchCamera(this.mainCamera, this.cutsceneParent.transform.Find("Progression1Phase").gameObject);
            else if(progressionPhase == 1) this.SwitchCamera(this.mainCamera, this.cutsceneParent.transform.Find("Progression2Phase").gameObject);
        }
    }

     private GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == name){
                return t.gameObject;
            }
        }
        return null;
    }

    private void SwitchCamera(GameObject cameraToEnable, GameObject cameraToDisable) {
        if(cameraToDisable != null) cameraToDisable.SetActive(false);
        if(cameraToEnable != null && cameraToDisable != null) cameraToEnable.SetActive(true);
        
    }

    private void setActiveStateObjects(bool state) {
        if(allPhaseObjects != null) foreach(GameObject g in allPhaseObjects) g.SetActive(state);
    }
}
