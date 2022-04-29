using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverworldLevelObject : MonoBehaviour
{
    public int levelIndex;
    public string levelKeyName;
    public string levelSceneName;

    
    public void DisplayLevelInfo() {
        GameObject.Find("LevelIndexText").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey("level") + " " + (levelIndex + 1).ToString();
        if(levelKeyName == "cheesemarket") GameObject.Find("LevelNameText").GetComponent<TextMeshProUGUI>().text = "Alkmaar " + GlobalGameHandler.GetTextByDictionaryKey(levelKeyName);
        else GameObject.Find("LevelNameText").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey(levelKeyName);
    }
}
