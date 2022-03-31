using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine;

public class GlobalGameHandler : MonoBehaviour
{
    public static GlobalGameHandler instance { get; private set; }

    private List<KeyValuePair<string,string>> translationDictionary;

    private List<string> languages;

    private string currentLanguage;

    private void Awake() 
    { 

        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) Destroy(this); 
        else {
            instance = this; 
            instance.languages = new List<string>();
            instance.translationDictionary = new List<KeyValuePair<string,string>>();
            instance.currentLanguage = "nederlands";
            DontDestroyOnLoad(this);
            InitTranslationDictionary();
        }
    }

    public static GlobalGameHandler GetInstance() {
        return instance;
    }

    private static void AddLanguage(string languageName) {
        instance.languages.Add(languageName);
    }

    public static string GetTextByDictionaryKey(string key) {
        string keyWithLanguage = key + "_" + instance.currentLanguage;
        foreach(KeyValuePair<string,string> pair in instance.translationDictionary) {
            if(pair.Key == keyWithLanguage) return pair.Value;
        }

        return "ERROR WORD NOT FOUND";
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
                Debug.Log(file);
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



