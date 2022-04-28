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
    public Vector3 StartPosition;

    void Start()
    {
      if(Player)
      {
        StartPosition = Player.transform.position;
      }
    }

    public void close()
    {
        HelpMenuUI.SetActive(false);
        if(PauseMenuUI){
            PauseMenuUI.SetActive(true);
        }
    }

    //teleporteert de speler naar het begin
    public void stuck()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player.transform.position = StartPosition;
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        HelpMenuUI.SetActive(false);
        Debug.Log(Cursor.lockState);
    }

    public void controls()
    {
        HowToPlayUI.SetActive(true);
    }

    public void objectives()
    {
        WhatDoIDoUI.SetActive(true);
    }
}
