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
        Debug.Log("Close Help");
        HelpMenuUI.SetActive(false);
        if(PauseMenuUI){
            PauseMenuUI.SetActive(true);
        }
    }

    public void stuck()
    {
        Debug.Log("Respawn player");
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
        Debug.Log("Go to help controls");
        HowToPlayUI.SetActive(true);
    }

    public void objectives()
    {
        Debug.Log("Go to help objectivs");
        WhatDoIDoUI.SetActive(true);
    }
}
