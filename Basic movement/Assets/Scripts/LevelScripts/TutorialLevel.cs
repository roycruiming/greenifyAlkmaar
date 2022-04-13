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

    public void Awake() {
        treeHasBeenTriggered = false;

        hasPlayedBefore = false;
    }

    public void triggerTreeFallingDown() {
        if(treeHasBeenTriggered == false) {
            foreach(GameObject tree in GameObject.FindGameObjectsWithTag("tutorialFallingDownTree")) tree.gameObject.GetComponent<TreeFallingDown>().StartFallingDown();
            
            treeHasBeenTriggered = true;
            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("tutorial space button"));
        }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
