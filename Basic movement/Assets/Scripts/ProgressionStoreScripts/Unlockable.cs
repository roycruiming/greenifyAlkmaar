using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    int id;
    float price;
    int unlockedInLevel;
    string exampleImageName;
    bool isUnlocked;

    public Unlockable(int id, float price, int unlockedInLevel, string imageName, bool unlocked) {
        this.id = id;

        if(PlayerPrefs.HasKey(id + "_unlockableInfo") == false) {
            //set the info and save the info to disk
            this.price = price;
            this.unlockedInLevel = unlockedInLevel;
            this.exampleImageName = imageName;
            this.isUnlocked = unlocked;
        }
        else {
            //load the info

        }


        
    }

    public void SaveInfoToDisk() {
        string unlockableInfoString = "";
        unlockableInfoString += this.price.ToString();
        unlockableInfoString += '_' + this.unlockedInLevel.ToString();
        unlockableInfoString += '_' + this.exampleImageName;
        unlockableInfoString += '_' + this.isUnlocked.ToString();

        Debug.Log(unlockableInfoString);


        //PlayerPrefs.Save();
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
