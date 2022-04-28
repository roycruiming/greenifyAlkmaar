using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldLevelSelectorHandler : MonoBehaviour
{

    GameObject[] levelObjects;
    public GameObject currentlySelectedLevel = null;

    private int currentlySelectedIndex;

    public void Awake() {
        levelObjects = GameObject.FindGameObjectsWithTag("LevelSelectorItem");
        if(PlayerPrefs.HasKey("lastOverworldIndex")) this.currentlySelectedIndex = PlayerPrefs.GetInt("lastOverworldIndex");
        else {
            this.currentlySelectedIndex = 0;
            PlayerPrefs.SetInt("lastOverworldIndex",currentlySelectedIndex);
        }
        
        this.currentlySelectedLevel = FindLevelObject(currentlySelectedIndex);
        FocusOnLevel(currentlySelectedIndex);

        //move all the level objects y - 200 to not make them visible anymore
        foreach(GameObject ob in levelObjects) ob.transform.position = new Vector3(ob.transform.position.x, ob.transform.position.y - 200, ob.transform.position.z);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if((currentlySelectedIndex + 1) < levelObjects.GetLength(0)) {
                this.currentlySelectedIndex++;
                FocusOnLevel(currentlySelectedIndex);
                
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if((currentlySelectedIndex - 1) > -1) {
                this.currentlySelectedIndex--;
                FocusOnLevel(currentlySelectedIndex);
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            //select current level
            // OverworldLevelObject lo = FindLevelObject(currentlySelectedIndex).GetComponents<OverworldLevelObject>();

            // SceneManager.LoadScene(lo.levelSceneName);
        }
        
        if(Input.GetKeyDown(KeyCode.C)) {
            //go to progression store

        }

        //         if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
//             if((currentLevelIndex + 1) < levelObjects.GetLength(0)) {
//                 //Debug.Log("right");
//                 this.currentLevelIndex = this.currentLevelIndex + 1;
//                 GetAndSetLevelSelectorByIndex(currentLevelIndex);
//             }
//         }
//         else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
//             if((currentLevelIndex - 1) > -1) {
//                 //Debug.Log("Left");
//                 this.currentLevelIndex = this.currentLevelIndex - 1;
//                 GetAndSetLevelSelectorByIndex(currentLevelIndex);
//             }
//         }
    }

    private void FocusOnLevel(int index) {
        foreach(GameObject levelObject in this.levelObjects) {
            if(levelObject.GetComponent<OverworldLevelObject>().levelIndex == index) {
                this.currentlySelectedLevel = levelObject; //for the camera to follow
                GameObject.Find("LevelSelectorMessageContainer").transform.position = new Vector3(levelObject.transform.position.x + 4.84f, GameObject.Find("LevelSelectorMessageContainer").transform.position.y, GameObject.Find("LevelSelectorMessageContainer").transform.position.z);
                 GameObject.Find("PressCToBuyContainer").transform.position = new Vector3(levelObject.transform.position.x + 5.81f, GameObject.Find("PressCToBuyContainer").transform.position.y, GameObject.Find("PressCToBuyContainer").transform.position.z);
                
                levelObject.GetComponent<OverworldLevelObject>().DisplayLevelInfo();
                PlayerPrefs.SetInt("lastOverworldIndex",currentlySelectedIndex); //save last position
            }
        }
    }

    private GameObject FindLevelObject(int index) {
        foreach(GameObject levelObject in this.levelObjects) {
            if(levelObject.GetComponent<OverworldLevelObject>().levelIndex == index) return levelObject;
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
