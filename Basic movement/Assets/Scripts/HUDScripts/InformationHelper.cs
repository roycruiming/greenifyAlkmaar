using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationHelper : MonoBehaviour
{

    public string informationTextDictionaryKey;
    
    public Sprite characterIcon;
    public Sprite spriteToShow; 

    public string GetTranslatedText() {
        return GlobalGameHandler.GetTextByDictionaryKey(this.informationTextDictionaryKey);
    }
    

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
