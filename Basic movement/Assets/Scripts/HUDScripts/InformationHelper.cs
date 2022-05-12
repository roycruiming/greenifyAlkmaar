using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationHelper : MonoBehaviour
{

    public string informationTextDictionaryKey;
    public bool isTree = false; 

    public bool keyTextIsSentence;
    
    public Sprite characterIcon;
    public Sprite spriteToShow; 

    public string GetTranslatedText() {
        return GlobalGameHandler.GetTextByDictionaryKey(this.informationTextDictionaryKey);
    }

    public List<string> GetMultipleTranslatedSentences() {
        return GlobalGameHandler.GetSentencesByDictionaryKey(this.informationTextDictionaryKey);
    }
    

    // Start is called before the first frame update
    void Awake()
    {
        //isTree = false; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
