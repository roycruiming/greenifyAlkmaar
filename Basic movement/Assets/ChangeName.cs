using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ChangeName : MonoBehaviour
{
    public GameObject textmesh;


    public void ChangeText(string text) {
        if (textmesh.GetComponent<TextMeshProUGUI>() == null) return;
        else textmesh.GetComponent<TextMeshProUGUI>().text = text; 


    }

}
