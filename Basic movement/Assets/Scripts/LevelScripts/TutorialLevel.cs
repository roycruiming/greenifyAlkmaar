using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour, LevelBasis
{
    public string levelName { get; set; }
    public int totalTasksCount { get; set; }
    public int completedTasksCount { get; set; }
    public int progressionPhase { get; set; }
    public bool hasPlayedBefore { get; set; }
    public List<GameObject> allPhaseObjects { get; set; }

    private bool treeHasBeenTriggered;
    private bool arrowHasBeenExplained;

    public void Awake() {
        treeHasBeenTriggered = false;
        arrowHasBeenExplained = false;

        hasPlayedBefore = false;
    }

    public void triggerTreeFallingDown() {
        if(treeHasBeenTriggered == false) {
            foreach(GameObject tree in GameObject.FindGameObjectsWithTag("tutorialFallingDownTree")) tree.gameObject.GetComponent<TreeFallingDown>().StartFallingDown();
            
            treeHasBeenTriggered = true;
            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("tutorial space button"));
        }
    }

    public void showcaseTutorialMessage() {
        if(arrowHasBeenExplained == false) GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,new List<string> {"This arrow here in our levels gives you a direction of where you need to go","The arrow always points towards a task that is not completed yet. If you don't know what to do just follow the arrow."});
        arrowHasBeenExplained = true;
    }

    public void initLevel()
    {
        throw new System.NotImplementedException();
    }

    public void saveProgress()
    {
        throw new System.NotImplementedException();
    }

    public void showcaseLevelProgression()
    {
        throw new System.NotImplementedException();
    }

    public void taskCompleted()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null, new List<string> {"Hello! Welcome I will teach you the basics of playing the game.",  "Use W A S D to move around and the mouse to look around!"});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
