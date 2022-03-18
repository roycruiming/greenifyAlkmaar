using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorHandler : MonoBehaviour
{
    int currentLevelIndex;
    GameObject[] levelObjects;
    GameObject levelSelectorOutline;
    bool right = false;
    bool left = false;

    public void Awake() {
        //initialize objects that are used for the navigation of the levels
        if(this.levelObjects == null) {
            levelObjects = GameObject.FindGameObjectsWithTag("LevelSelectorItem");
            levelSelectorOutline = GameObject.Find("level-selector-outline");
            currentLevelIndex = 0;
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
                Debug.Log("right");
                this.currentLevelIndex = this.currentLevelIndex + 1;
                GetAndSetLevelSelectorByIndex(currentLevelIndex);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            Debug.Log(levelObjects.GetLength(0));
            if((currentLevelIndex - 1) > -1) {
                Debug.Log("Left");
                this.currentLevelIndex = this.currentLevelIndex - 1;
                GetAndSetLevelSelectorByIndex(currentLevelIndex);
            }
        }
    }

    private async void GetAndSetLevelSelectorByIndex(int index) {
        GameObject indexObject = null;
        foreach(GameObject g in this.levelObjects) {
            if(g.GetComponent<LevelSelectorObject>().index == index) {
                indexObject = g;
                break;
            }
        }

        if(indexObject != null) {
            levelSelectorOutline.transform.position = new Vector3(indexObject.transform.position.x, levelSelectorOutline.transform.position.y, indexObject.transform.position.z);

        }
        //else return null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
