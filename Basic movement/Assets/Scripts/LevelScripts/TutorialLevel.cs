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

    private bool treeHasBeenTriggered = false;
    private bool arrowHasBeenExplained = false;
    private bool helperHasBeenExplained = false;

    public void Awake() {
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
        if(arrowHasBeenExplained == false) GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("tutorial direction arrow"));
        arrowHasBeenExplained = true;
        print(GetComponent<DirectionalArrow>());
        GameObject.Find("3RD Person").GetComponent<ArrowVisible>().arrowVisible = true;
    }

    public void showcaseGeneralHelperExplanation() {
        if(helperHasBeenExplained == false) {
            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("tutorial helper text"));
        }
        helperHasBeenExplained = true;
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
        GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null, GlobalGameHandler.GetSentencesByDictionaryKey("tutorial level intro"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
