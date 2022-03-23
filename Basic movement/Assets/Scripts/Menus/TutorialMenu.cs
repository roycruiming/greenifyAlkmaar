using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject HelpMenuUI;
    public GameObject PauseMenuUI;

    public GameObject HowToPlayUI;
    public GameObject WhatDoIDoUI;

    public GameObject Player;
    public float StartX;
    public float StartY;
    public float StartZ;

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
        Player.transform.position = new Vector3(StartX, StartY, StartZ);
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        HelpMenuUI.SetActive(false);
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
