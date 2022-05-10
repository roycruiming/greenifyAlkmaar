using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class GlobalGameHandler : MonoBehaviour
{
    public static GlobalGameHandler instance { get; private set; }

    private List<KeyValuePair<string,string>> translationDictionary;
    public List<Unlockable> allUnlockables;

    private int SelectedCharacterUnlockableId;

    private List<string> languages;

    private string currentLanguage;

    private int totalPlayerCoints;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) Destroy(this); 
        else {
            instance = this; 
            instance.languages = new List<string>();
            instance.translationDictionary = new List<KeyValuePair<string,string>>();
            if(PlayerPrefs.HasKey("settings_language")) instance.currentLanguage = PlayerPrefs.GetString("settings_language");
            else {
                instance.currentLanguage = "english";
                PlayerPrefs.SetString("settings_language","english");
            }

            if(PlayerPrefs.HasKey("settings_directionalArrow") == false) PlayerPrefs.SetInt("settings_directionalArrow",1);

            DontDestroyOnLoad(this.gameObject);
            InitTranslationDictionaryBuildFunctional();
            InitUnlockAbles();

            LoadSaveGameInfo();


        }
    }

    private void LoadSaveGameInfo() {
        if(PlayerPrefs.HasKey("playerTotalCoints") == false) {
            PlayerPrefs.SetInt("playerTotalCoints", 0);
            this.totalPlayerCoints = 0;
            PlayerPrefs.Save();
        }
        else this.totalPlayerCoints = PlayerPrefs.GetInt("playerTotalCoints");


        if(PlayerPrefs.HasKey("ActiveCharacterUnlockId") == false) {
            PlayerPrefs.SetInt("ActiveCharacterUnlockId",-1);
            this.SelectedCharacterUnlockableId = -1;
            PlayerPrefs.Save();
        }
        else this.SelectedCharacterUnlockableId = PlayerPrefs.GetInt("ActiveCharacterUnlockId");
    }

    public static void ChangeSelectedCharacterByUnlockId(int unlockableId) {
        instance.SelectedCharacterUnlockableId = unlockableId;
        PlayerPrefs.SetInt("ActiveCharacterUnlockId",unlockableId);
        PlayerPrefs.Save();
        print("new character equipped");
    }

    public static bool PlayerWantsDirectionalArrow() {
        if(PlayerPrefs.HasKey("settings_directionalArrow") == false) {
            PlayerPrefs.SetInt("settings_directionalArrow",1);
            return true;
        }
        else {
            if(PlayerPrefs.GetInt("settings_directionalArrow") == 1) return true;
            else return false;
        }
    }

    public static void ToggleDirectionalArrowSetting() {
        if(PlayerPrefs.HasKey("settings_directionalArrow") == false) PlayerPrefs.SetInt("settings_directionalArrow",1);
        else {
            if(PlayerPrefs.GetInt("settings_directionalArrow") == 1) PlayerPrefs.SetInt("settings_directionalArrow",0);
            else PlayerPrefs.SetInt("settings_directionalArrow",1);
        }

    }

    private void InitUnlockAbles() {
        instance.allUnlockables = new List<Unlockable>();

        PlayerPrefs.DeleteAll(); //just for testing remove later!

        //NOTE: WHEN ADDING AN UNLOCKABLE TO THIS LIST UP THE FIRST INTEGER BY 1
        //id price unlockedInLevel imageName unlocked unlockableType isPurchased
        //Every class gets saved from the constructor
        instance.allUnlockables.Add(new Unlockable(0,1644,0,"clown face",true,UnlockableType.character, "man+clown")); //set the initial info, if info has already been set constructor loads the saved info and initializes the object
        instance.allUnlockables.Add(new Unlockable(1,2030,1,"super woman",false,UnlockableType.character, "woman+superhero")); 
        instance.allUnlockables.Add(new Unlockable(2,550,1,"ninja man",false,UnlockableType.character, "man+ninja")); 
        instance.allUnlockables.Add(new Unlockable(3,320,2,"astronaut man",false,UnlockableType.character, "man+astronaut")); 
        instance.allUnlockables.Add(new Unlockable(4,320,2,"police woman",false,UnlockableType.character, "woman+police")); 
        instance.allUnlockables.Add(new Unlockable(5,320,3,"test3",false,UnlockableType.character)); 

        //save the total count of unlockables
        PlayerPrefs.SetInt("UnlockableCount",instance.allUnlockables.Count);
        
        GlobalGameHandler.GivePlayerCoints(1900);
    }

    public static int GetTotalPlayerCointsAmount() {
        return instance.totalPlayerCoints;
    }

    public static void GivePlayerCoints(int amount) {
        instance.totalPlayerCoints += amount;
        PlayerPrefs.SetInt("playerTotalCoints", instance.totalPlayerCoints);
        PlayerPrefs.Save();
    }

    public static void LowerPlayerCoints(int amount) {
        instance.totalPlayerCoints -= amount;
        PlayerPrefs.SetInt("playerTotalCoints", instance.totalPlayerCoints);
        PlayerPrefs.Save();
    }

    public static List<Unlockable> GetAllUnlockablesInfo() {
        return instance.allUnlockables;
    }

    public static GlobalGameHandler GetInstance() {
        return instance;
    }

    private static void AddLanguage(string languageName) {
        instance.languages.Add(languageName);
    }

    public static void ChangeLanguage(string languageName) {
        if(instance.languages.Contains(languageName)) {
            instance.currentLanguage = languageName;
            PlayerPrefs.SetString("settings_language",languageName);
        }
    }

    public static List<string> GetLanguagesList() {
        return instance.languages;
    }

    public static string GetCurrentLanguage() {
        return instance.currentLanguage;
    }

    public static string GetTextByDictionaryKey(string key) {
        string keyWithLanguage = key + "_" + instance.currentLanguage;
        foreach(KeyValuePair<string,string> pair in instance.translationDictionary) {
            if(pair.Key == keyWithLanguage) return pair.Value;
        }

        return "ERROR WORD NOT FOUND";
    }

    private static void debugIets() {
        foreach(KeyValuePair<string,string> pair in instance.translationDictionary) print("Key = " + pair.Key + ", Value = " + pair.Value);
    }

    public static List<string> GetSentencesByDictionaryKey(string key) {
        string keyWithLanguage = key + "_" + instance.currentLanguage;
        string sentence = "";
        foreach(KeyValuePair<string,string> pair in instance.translationDictionary) {
            if(pair.Key == keyWithLanguage) sentence = pair.Value;
        }

        if(sentence.Contains(";")) return sentence.Split(';').ToList(); //sentences are splitted by ';' character
        else return new List<string> { sentence };
    }

    private static void AddTranslationWord(string key, string value) {
        instance.translationDictionary.Add(new KeyValuePair<string,string>(key, value));
    }

    private List<KeyValuePair<string, string>> InitTranslationDictionaryBuildFunctional() {
        //System.Object[] files = Resources.LoadAll("TranslationFiles/Languages");
        TextAsset[] translationFiles = Resources.LoadAll<TextAsset>("TranslationFiles/Languages");
        foreach(TextAsset textAsset in translationFiles) {
            string[] allCsvLines = Regex.Split ( textAsset.text, "\n|\r|\r\n" );
            foreach(string line in allCsvLines) {
                if(!string.IsNullOrEmpty(line)) {
                    string[] stringData = line.Split(',');
                    if(stringData != null && stringData.GetLength(0) > 1) {
                            if(stringData[0] == "_language_") {
                                //add languages to languages list
                                GlobalGameHandler.AddLanguage(stringData[1]);
                                currentLanguage = stringData[1];
                            }
                            else {
                                //add key and word to the words list
                                //the key is the keyword + current language so for example:
                                //  key:    'start game_english'    value: 'start game'
                                //  key:    'start game_nederlands'    value: 'start spel'
                                if(currentLanguage != null) {
                                    GlobalGameHandler.AddTranslationWord(stringData[0] + '_' + currentLanguage, stringData[1]);
                                    //Debug.Log("Added to the words dictionary: key= " + values[0] + '_' + currentLanguage + " , values= " + values[1]);
                                }

                            }
                    }
                }
            }
        }
        //foreach(System.Object s in files) print("test");
        return null;
    }

    private List<KeyValuePair<string, string>> InitTranslationDictionary()
    {
        string PathToLanguagesDir = Application.dataPath + "/Resources/TranslationFiles/Languages";
        //check if the file is not unity generated meta file and check if it is a csv file.
        string[] files = System.IO.Directory.GetFiles(PathToLanguagesDir);
        

        foreach (string file in files)
        {
            if (file.Contains("meta") == false && file.Contains("csv"))
            {
                var reader = new StreamReader(File.OpenRead(file));
                string currentLanguage = null;
                while(!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    //check if the line split resulted in a not null object
                    if(values != null) {
                        //check if it contains 2 values 1 key value and other language value
                        if(values.GetLength(0) > 1) {
                            //when reading the values[0] and it is _language_ , means that values[1] is the language name
                            if(values[0] == "_language_") {
                                //add languages to languages list
                                GlobalGameHandler.AddLanguage(values[1]);
                                currentLanguage = values[1];
                            }
                            else {
                                //add key and word to the words list
                                //the key is the keyword + current language so for example:
                                //  key:    'start game_english'    value: 'start game'
                                //  key:    'start game_nederlands'    value: 'start spel'
                                if(currentLanguage != null) {
                                    GlobalGameHandler.AddTranslationWord(values[0] + '_' + currentLanguage, values[1]);
                                    //Debug.Log("Added to the words dictionary: key= " + values[0] + '_' + currentLanguage + " , values= " + values[1]);
                                }

                            }
                        }
                    }


                    // if(values.GetLength(0) > 1) Debug.Log(values[0] + " = " + values[1]);
                    // else Debug.Log("taal = " + values[0]);
                    //foreach(string s in values) Debug.Log(s);
                }
            }
        }

        return null;
    }
}

    //OLD UNLOCKABLE SCRIPT



    //OLD UNLOCKABLE SCRIPT
    // public static Unlockable GetUnlockableById(int unlockableId) {
    //     for(int i = 0; i < instance.allUnlockables.Count; i++) if(instance.allUnlockables[i].id == unlockableId) { 
    //         print(instance.allUnlockables[i].id);
    //         Unlockable returningUn = instance.allUnlockables[i];
    //         return returningUn; 
    //     }
    //     //foreach(Unlockable unlockable in instance.allUnlockables) if(unlockable.id == unlockableId) return unlockable;

    //     return null;
    // }

//remove this at some point
public enum Language {
    Dutch, English
}


// Translation kit:

// Bepaal het geslacht van de taal waarin het geschreven wordt.


// Elk woord heeft een engelse-key
// al deze engelse-keys zijn dus woorden als
// 'start game'

// waarnaar er een nederlandse CSV (excel) file komt die dan al deze keys bevat en de vertaling

// column1		column 2
// 'start game'	'start spel'

// Elke translation CSV file bevat de taal naam
// 'language_name'	= 'Nederlands'


// List containing:
// key: start game  values: en_start game, nl_start spel, de_spiel starten


//METHOD get text by key, and the value that is returned is decided by the language that is currently active
//add a parameter which will capatilize every word, or the first word or none etc.

//Need an object script that will on awake check which type of text component the gameobject contains
//and it will contain the key of the text that has to be found



