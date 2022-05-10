using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KaasmarktLevel : MonoBehaviour, LevelBasis
{
    public string levelName { get; set; }
    public int totalTasksCount { get; set; }
    public int completedTasksCount { get; set; }
    public bool hasPlayedBefore { get; set; }

    public int progressionPhase { get; set; }

    private GameObject cutsceneParent;
    private GameObject mainCamera;
    public List<GameObject> allPhaseObjects { get; set; }

    private List<List<GameObject>> allPhaseObjectsList = new List<List<GameObject>>();

    private bool blinkProgressObjects;
    private int objectBlinkCounter;
    private int amountOfBlinks = 18;

    private float elapsedTime = 0f;

    public void Awake() {
        GameObject.Find("splash").GetComponent<FadeInOutScript>().MakeInVisible();
        
        initLevel();
        
        //StartCoroutine(showcaseIntroCutscene());


        //intro cutscene


    }

    public void initLevel()
    {
        this.mainCamera = GameObject.Find("Main Camera");
        this.cutsceneParent = GameObject.Find("cutscenesHolder");

        this.levelName = GlobalGameHandler.GetTextByDictionaryKey("cheesemarket");

        totalTasksCount = -1;

        completedTasksCount = 0;

        hasPlayedBefore = false;

        progressionPhase = -1;

        objectBlinkCounter = 0;

        blinkProgressObjects = false;

        //get all phase objects
        //save all phase 1 (index = 0) objects and hide them
        this.allPhaseObjectsList.Add(GameObject.FindGameObjectsWithTag("ProgressPhase1Object").ToList<GameObject>());
        this.allPhaseObjectsList.Add(GameObject.FindGameObjectsWithTag("ProgressPhase2Object").ToList<GameObject>());
        foreach(GameObject g in this.allPhaseObjectsList[0]) g.SetActive(false);
        foreach(GameObject g in this.allPhaseObjectsList[1]) g.SetActive(false);
    }

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
            this.amountOfBlinks = 18; //back to default
            setActiveStateObjects(true);

            //switch camera back
            if(progressionPhase == 0) this.SwitchCamera(this.mainCamera, this.cutsceneParent.transform.Find("Progression1Phase").gameObject);
            else if(progressionPhase == 1) this.SwitchCamera(this.mainCamera, this.cutsceneParent.transform.Find("Progression2Phase").gameObject);
        }
    }

    private void setActiveStateObjects(bool state) {
        if(allPhaseObjects != null) foreach(GameObject g in allPhaseObjects) g.SetActive(state);
    }

    IEnumerator showcaseIntroCutscene() {
        
        this.mainCamera.SetActive(false);
        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        //GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("intro de meent"));
        yield return new WaitForSeconds(18);

        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(false);
        this.mainCamera.SetActive(true);
        yield return null;

    }

    public void showSplashEffectAndSound() {
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(((AudioClip)Resources.Load("Sounds/water_splash")));

        StartCoroutine(showcaseSplashEffect());
    }

    public IEnumerator showcaseSplashEffect() {
        GameObject.Find("splash").GetComponent<FadeInOutScript>().MakeVisible();
        yield return new WaitForSeconds(0.56f);

        GameObject.Find("splash").GetComponent<FadeInOutScript>().StartFadingOut();
        
    }

    private void SwitchCamera(GameObject cameraToEnable, GameObject cameraToDisable) {
        if(cameraToDisable != null) cameraToDisable.SetActive(false);
        if(cameraToEnable != null) cameraToEnable.SetActive(true);
        
    }

    public void saveProgress()
    {
        throw new System.NotImplementedException();
    }

    public void showcaseLevelProgression()
    {
        progressionPhase++; //up the progression level phase
        if(allPhaseObjectsList[progressionPhase] != null) {
            //initiate the blinking of the new greener level-props
            allPhaseObjects = allPhaseObjectsList[progressionPhase];
            this.blinkProgressObjects = true;
            
            //add rewards if needed=

            if(progressionPhase == 0) {
                this.cutsceneParent.transform.Find("Progression1Phase").GetComponent<Animator>().SetTrigger("phase1");
                this.amountOfBlinks = 26;
                SwitchCamera(this.cutsceneParent.transform.Find("Progression1Phase").gameObject,this.mainCamera);
                GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("cheesemarket phase 1 text"));
                //reward the player
                if(GameObject.Find("HUDCanvas").GetComponent<HUDController>() != null) GameObject.Find("HUDCanvas").GetComponent<HUDController>().showcaseAndUnlockUnlockable(3);
                GlobalGameHandler.GivePlayerCoints(Random.Range(801,870));
            }
            else if(progressionPhase == 1) {
                this.cutsceneParent.transform.Find("Progression2Phase").GetComponent<Animator>().SetTrigger("phase2");
                SwitchCamera(this.cutsceneParent.transform.Find("Progression2Phase").gameObject,this.mainCamera);

                //reward the player
                if(GameObject.Find("HUDCanvas").GetComponent<HUDController>() != null) GameObject.Find("HUDCanvas").GetComponent<HUDController>().showcaseAndUnlockUnlockable(4);
                GlobalGameHandler.GivePlayerCoints(Random.Range(801,870));
            }
        }
    }

    public void taskCompleted()
    {
        throw new System.NotImplementedException();
    }
}
