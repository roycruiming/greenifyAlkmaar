using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class InformationHelper : MonoBehaviour
{

    public string informationTextDictionaryKey;
    public bool isTree = false;

    public bool keyTextIsSentence;

    public Sprite characterIcon;
    public Sprite spriteToShow;

    List<string> teachingWords = new List<string>{
      "wind turbine basic explanation",
      "recycle icon basic explanation",
      "solar panel explanation",
      "meent npc 8",
      "tutorial casual guy text"
    };

    public string GetTranslatedText() {
        SendAnalytics();
        return GlobalGameHandler.GetTextByDictionaryKey(this.informationTextDictionaryKey);
    }

    public List<string> GetMultipleTranslatedSentences() {
        SendAnalytics();
        return GlobalGameHandler.GetSentencesByDictionaryKey(this.informationTextDictionaryKey);
    }


    public void SendAnalytics(){
      if(teachingWords.Contains(informationTextDictionaryKey))
      {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
        "TalkedToNPC",
        new Dictionary<string, object> {
          {"NPC", informationTextDictionaryKey},
        });
      }
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
