using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextTranslationScript : MonoBehaviour
{
    public string textDutch;
    public string textEnglish;

    public void Awake() {
        // //try to change the text mesh if exists

        // //check of text os TextMesh3d component
        // if(this.GetComponent<TextMesh>() != null) {
        //     if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) this.GetComponent<TextMesh>().text = this.textDutch;
        //     else this.GetComponent<TextMesh>().text = this.textEnglish;
        // }
        // //check if component is ui.Text component
        // if(this.GetComponent<UnityEngine.UI.Text>() != null) {
        //     if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) this.GetComponent<UnityEngine.UI.Text>().text = this.textDutch;
        //     else this.GetComponent<UnityEngine.UI.Text>().text = this.textEnglish;
        // }
        // //check if component is TMPro.TextMeshProUGUI component
        // if(this.GetComponent<TextMeshProUGUI>() != null) {
        //     if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) this.GetComponent<TextMeshProUGUI>().text = this.textDutch;
        //     else this.GetComponent<TextMeshProUGUI>().text = this.textEnglish;
        // }
    }

    public string getText() {
        // if(GlobalGameHandler.GetInstance().currentLanguage == Language.Dutch) return this.textDutch;
        // else return this.textEnglish;
        return "ERROR";
    }
}
