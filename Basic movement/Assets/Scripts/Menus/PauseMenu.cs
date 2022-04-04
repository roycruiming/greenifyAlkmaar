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
    public GameObject SettingMenu;

    public DirectionalArrow arrow;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Cursor.visible = false;
                Resume();
            } else
            {
                Cursor.visible = true;
                Pause();
            }

        }
    }

        public void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void Options()
        {
            SwitchVisibility(SettingMenu, true);
        }

        public void Help()
        {
            SwitchVisibility(HelpMenu, true);
            SwitchVisibility(PauseMenuUI, false);
        }

        public void ExitLevel()
        {
            GameIsPaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(ExitTo);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        void SwitchVisibility(GameObject panel, bool visbility)
        {
            if (panel)
            {
                panel.SetActive(visbility);
            }
        }
}
