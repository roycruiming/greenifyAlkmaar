using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject HelpMenu;

    public void StartGame()
    {
        Debug.Log("Start the game!");
        SceneManager.LoadScene("Mainmap-Scene");
    }

    public void Tutorial()
    {
        Debug.Log("Open Tutorial");
        HelpMenu.SetActive(true);
    }

    public void Options()
    {
        Debug.Log("Open Options");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
