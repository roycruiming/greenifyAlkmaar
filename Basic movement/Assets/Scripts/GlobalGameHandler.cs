using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameHandler
{
    private GlobalGameHandler() { 
        this.currentLanguage = Language.English;
    }

    public Language currentLanguage;

    private static GlobalGameHandler _instance;

    //dont touch this it initiates the instance
    public static GlobalGameHandler GetInstance() {
        if(_instance == null) {
            _instance = new GlobalGameHandler();
        }
        
        return _instance;
    }

    //call this function to change the language of the game.
    public static void SwitchLanguage() {
        GlobalGameHandler h = GlobalGameHandler.GetInstance();
        
        if(h.currentLanguage == Language.Dutch) h.currentLanguage = Language.English;
        else h.currentLanguage = Language.Dutch;
    }


}

public enum Language {
    Dutch, English
}
