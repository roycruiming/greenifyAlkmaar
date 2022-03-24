using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LevelSelectorHandler : MonoBehaviour
{
    public int currentLevelIndex;
    GameObject[] levelObjects;
    GameObject levelSelectorOutline;
    Material unlockedOutline;
    Material lockedOutline;
    GameObject levelInfoContainer;
    GameObject lockedIcon;
    GameObject pressButTextObj;
    bool rotating = false;

    public void Awake() {
        //initialize objects that are used for the navigation of the levels
        if(this.levelObjects == null) {
            levelObjects = GameObject.FindGameObjectsWithTag("LevelSelectorItem");
            levelSelectorOutline = GameObject.Find("level-selector-outline");
            levelInfoContainer = GameObject.FindGameObjectWithTag("LevelInfoContainer");
            currentLevelIndex = 0;
            this.unlockedOutline = Resources.Load("SelectionUnlocked", typeof(Material)) as Material;
            this.lockedOutline = Resources.Load("SelectionLocked", typeof(Material)) as Material;
            this.lockedIcon = GameObject.Find("LockedIcon");
            this.pressButTextObj = GameObject.Find("PressButtToPlay");
        }

        // if(levelSelectorOutline != null) {
        //     Debug.Log("gevonden92)");
        // }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if((currentLevelIndex + 1) < levelObjects.GetLength(0)) {
                //Debug.Log("right");
                this.currentLevelIndex = this.currentLevelIndex + 1;
                GetAndSetLevelSelectorByIndex(currentLevelIndex);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if((currentLevelIndex - 1) > -1) {
                //Debug.Log("Left");
                this.currentLevelIndex = this.currentLevelIndex - 1;
                GetAndSetLevelSelectorByIndex(currentLevelIndex);
            }
        }

        //if button x is pressed launch scene
        if(Input.GetKeyDown(KeyCode.E)) {
            //get the current levelselectorObject
            GameObject selectedLevel = this.getGameObjectByIndex(this.currentLevelIndex);
            LevelSelectorObject currentLevelInfo = selectedLevel.GetComponent<LevelSelectorObject>();

            if(currentLevelInfo.isUnlocked) {
                //launch scene because level is unlocked and user pressed the correct button
                if(currentLevelInfo != null) SceneManager.LoadScene("DeMeent");
            }
        }
    }

    private void GetAndSetLevelSelectorByIndex(int index) {
        GameObject.Find("MainMapCharacter").GetComponent<MainMapCharacter>().AddPath(index);
        GameObject indexObject = this.getGameObjectByIndex(index);

        if(indexObject != null) {
            levelSelectorOutline.transform.position = new Vector3(indexObject.transform.position.x, levelSelectorOutline.transform.position.y, indexObject.transform.position.z);
            LevelSelectorObject selectedLevelInfo = indexObject.GetComponent<LevelSelectorObject>();

            //move level info container
            levelInfoContainer.transform.position = new Vector3(indexObject.transform.position.x - 25, levelInfoContainer.transform.position.y, indexObject.transform.position.z + 207);
            if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) SetLevelName(selectedLevelInfo.levelNameDutch);
            else if(GlobalGameHandler.GetInstance().currentLanguage == Language.English) SetLevelName(selectedLevelInfo.levelNameEnglish);

            //handle the visual state of the unlocked/locked level
            this.lockedOrUnlockedVisualStateHandler(selectedLevelInfo, indexObject);
        }
        //else return null;
    }

    private GameObject getGameObjectByIndex(int index) {
        foreach(GameObject g in this.levelObjects) {
            if(g.GetComponent<LevelSelectorObject>().index == index) {
                return g;
            }
        }

        return null; //failed something went wrong
    }

    private void lockedOrUnlockedVisualStateHandler(LevelSelectorObject selectedLevelInfo, GameObject targetObject)
    {
        //set material of the outline object
        Material outlineObjectMaterial = null;
        if (selectedLevelInfo.isUnlocked) outlineObjectMaterial = this.unlockedOutline;
        else outlineObjectMaterial = this.lockedOutline;
        
        levelSelectorOutline.GetComponent<Renderer>().material = outlineObjectMaterial;

        //show or hide locked image Icon
        if (selectedLevelInfo.isUnlocked) this.lockedIcon.transform.position = new Vector3(targetObject.transform.position.x - 21, -2, targetObject.transform.position.z + 15);
        else this.lockedIcon.transform.position = new Vector3(targetObject.transform.position.x - 21, 2 , targetObject.transform.position.z + 15);

        //if level is locked set the text from press 'E' to play naar niks
        //gets the text in the right language
        if(selectedLevelInfo.isUnlocked) this.pressButTextObj.GetComponent<UnityEngine.UI.Text>().text = this.pressButTextObj.GetComponent<TextTranslationScript>().getText();
        else {
            string lockedLevelText = "";
             if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) lockedLevelText = "Level is nog niet unlocked";
             else lockedLevelText = "Level is not unlocked yet";

             this.pressButTextObj.GetComponent<UnityEngine.UI.Text>().text = lockedLevelText;
        }
    }   

    private void SetLevelName(string name) {
        GameObject.Find("LevelNameDisplay").GetComponent<UnityEngine.UI.Text>().text = name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
