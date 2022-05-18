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
    public AudioSource OpenSound;
    public AudioSource ClickSound;


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
        //Continues the game
        public void Resume()
        {
            if(!PuzzleController.PuzzlePlaying)
            {
              Cursor.lockState = CursorLockMode.Locked;
            }

            foreach (Transform child in transform)
            {
                if(child.gameObject.GetComponent<AudioSource>() == null)
                {
                  child.gameObject.SetActive(false);
                }
            }
            Time.timeScale = 1f;
            AudioListener.pause = false;
            GameIsPaused = false;

        }

        //Pauses the game
        public void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            AudioListener.pause = true;
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
            AudioListener.pause = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(ExitTo);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            GameIsPaused = false;
            AudioListener.pause = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("MainMenu");
        }

        void SwitchVisibility(GameObject panel, bool visbility)
        {
            if (panel)
            {
                panel.SetActive(visbility);
            }
        }

        public void playSound()
        {
          ClickSound.Play();
        }

}
