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
    public GameObject SettingMenu;
    public AudioSource ClickSound;

    public void StartGame()
    {
        SwitchVisibility(StartButtons, MainButtons);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial-Level");
    }

    public void Options()
    {
        SettingMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("OverworldMapV2");
    }

    public void Multiplayer()
    {
      SceneManager.LoadScene("LoadingScreen");
    }

    public void BackToMainMenu()
    {
        SwitchVisibility(MainButtons, StartButtons);
    }

    public void shop()
    {
      SceneManager.LoadScene("Progression-store");
    }

    //Switches the visibility of the given 2 objects
    void SwitchVisibility(GameObject object1, GameObject object2)
    {
        object1.SetActive(true);
        object2.SetActive(false);
    }

}
