using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject HelpMenuUI;
    public GameObject PauseMenuUI;

    public GameObject HowToPlayUI;
    public GameObject WhatDoIDoUI;

    public void close()
    {
        Debug.Log("Close Help");
        HelpMenuUI.SetActive(false);
        if(PauseMenuUI){
            PauseMenuUI.SetActive(true);
        }
    }

    public void stuck()
    {
        Debug.Log("Respawn player");
    }

    public void controls()
    {
        Debug.Log("Go to help controls");
        HowToPlayUI.SetActive(true);
    }

    public void objectives()
    {
        Debug.Log("Go to help objectivs");
        WhatDoIDoUI.SetActive(true);
    }
}
