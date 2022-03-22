using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public string ExitTo = "";

    public GameObject PauseMenuUI;
    public GameObject HelpMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);    
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Options()
    {
        Debug.Log("Go to options");

    }

    public void Help()
    {
       Debug.Log("Go to help");
       HelpMenu.SetActive(true);
       PauseMenuUI.SetActive(false);
    }

    public void ExitLevel()
    {
        Debug.Log("Go to level select");
        Time.timeScale = 1f;
        SceneManager.LoadScene(ExitTo);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
