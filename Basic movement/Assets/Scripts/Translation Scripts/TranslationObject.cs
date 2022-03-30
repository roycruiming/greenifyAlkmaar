using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationObject : MonoBehaviour
{
    public string translationKey;

    void Start()
    {
        //check of text os TextMesh3d component
        if (GetComponent<TextMesh>() != null)
        {
            Debug.Log("TextMesh");
            GetComponent<TextMesh>().text = GlobalGameHandler.GetTextByDictionaryKey(this.translationKey);
        }
        //check if component is ui.Text component
        if (GetComponent<UnityEngine.UI.Text>() != null)
        {
            Debug.Log("UnityEngine.UI.Text");
            GetComponent<UnityEngine.UI.Text>().text = GlobalGameHandler.GetTextByDictionaryKey(this.translationKey);
        }
        //check if component is TMPro.TextMeshProUGUI component
        if (GetComponent<TextMeshProUGUI>() != null)
        {
            Debug.Log("TextMeshProUGUI");
            GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey(this.translationKey);
        }

        //check if component is TMPro.TextMeshProUGUI component
        if (GetComponent<TextMesh>() != null)
        {
            Debug.Log("TextMesh");
            GetComponent<TextMesh>().text = GlobalGameHandler.GetTextByDictionaryKey(this.translationKey);
        }
    }

}
