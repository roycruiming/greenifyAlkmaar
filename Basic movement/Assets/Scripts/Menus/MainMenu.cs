using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject HelpMenu;
    public GameObject MainButtons;
    public GameObject StartButtons;

    public void StartGame()
    {
        SwitchVisibility(StartButtons, MainButtons);
    }

    public void Tutorial()
    {
        HelpMenu.SetActive(true);
    }

    public void Options()
    {
        Debug.Log("Open Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("Mainmap-Scene");
    }

    public void BackToMainMenu()
    {
        SwitchVisibility(MainButtons, StartButtons);
    }

    void SwitchVisibility(GameObject object1, GameObject object2)
    {
        object1.SetActive(true);
        object2.SetActive(false);
    }
}
