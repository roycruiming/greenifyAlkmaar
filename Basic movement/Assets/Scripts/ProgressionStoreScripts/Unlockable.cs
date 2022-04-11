using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnlockableType { character, powerUp }

public class Unlockable : MonoBehaviour
{
    public int id;
    public float price;
    public int unlockedInLevel;
    public string exampleImageName;
    public bool isUnlocked;
    public bool isPurchased;

    public UnlockableType type;

    public Unlockable(int id, float price, int unlockedInLevel, string imageName, bool unlocked, UnlockableType unlockableType, bool isPurchased = false) {
        this.id = id;

        if(PlayerPrefs.HasKey(id + "_unlockableInfo") == false) {
            //set the info and save the info to disk
            this.price = price;
            this.unlockedInLevel = unlockedInLevel;
            this.exampleImageName = imageName;
            this.isUnlocked = unlocked;
            this.type = unlockableType;
            this.isPurchased = isPurchased; //default false
            if(isPurchased == true) this.isUnlocked = true; //prevent mistakes (error handling)

            //Debug.Log("Registered");
            SaveInfoToDisk();
        }
        else {
            //load the info
            // 0 = price , 1 = unlocked , 2 = imageName string , 3 = unlocked bool , 4 = unlockable type
            string[] unlockableInfo = PlayerPrefs.GetString(id + "_unlockableInfo").Split('_');
            this.price = float.Parse(unlockableInfo[0]);
            this.unlockedInLevel = int.Parse(unlockableInfo[1]);
            this.exampleImageName = unlockableInfo[2];
            this.isUnlocked = bool.Parse(unlockableInfo[3]);
            this.type = (UnlockableType) int.Parse(unlockableInfo[4]);
            this.isPurchased = bool.Parse(unlockableInfo[5]);
            
            //Debug.Log("From disk");
        }


        
    }

    private void SaveInfoToDisk() {
        string unlockableInfoString = "";
        unlockableInfoString += this.price.ToString();
        unlockableInfoString += '_' + this.unlockedInLevel.ToString();
        unlockableInfoString += '_' + this.exampleImageName;
        unlockableInfoString += '_' + this.isUnlocked.ToString();
        unlockableInfoString += '_' + ((int)this.type).ToString();
        unlockableInfoString += '_' + this.isPurchased.ToString();

        // string[] splittedStr = unlockableInfoString.Split('_');
        // for(int i = 0; i < splittedStr.GetLength(0); i++) Debug.Log(i + " = " + splittedStr[i]);
        // Debug.Log(unlockableInfoString);

        PlayerPrefs.SetString(id + "_unlockableInfo", unlockableInfoString);
        PlayerPrefs.Save();
    }

    public void UpdateInfoToDisk() {
        this.SaveInfoToDisk();
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
