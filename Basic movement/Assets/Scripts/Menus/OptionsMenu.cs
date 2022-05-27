using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Dropdown LanguageDropdown;
    public GameObject optionMenu;
    public Toggle ToggleCheckBox;
    public GameObject AreYouSure;

    public string LanguageSelected;
    int LanguageSelectedIndex;
    List<string> languages;
    bool LanguageStarted = false;

    //set the correct values of the options menu
    void Start()
    {
      ToggleCheckBox = transform.Find("ArrowToggle").GetComponent<Toggle>();
      //checks if the player has toggled on/off the directional arrow from the save game
      if(ToggleCheckBox){
        if(GlobalGameHandler.PlayerWantsDirectionalArrow()){
          ToggleCheckBox.isOn = true;
        } else {
          ToggleCheckBox.isOn = false;
        }
      }

      //Makes the dropdown empty and adds the correct languages
      if(LanguageDropdown)
      {
        languages = GlobalGameHandler.GetLanguagesList();
        LanguageDropdown.ClearOptions();
        LanguageDropdown.AddOptions(languages);

        //Checks for the current language and sets the value to that language
        for (int i = 0; i < languages.Count; i++)
        {
          if(languages[i] == GlobalGameHandler.GetCurrentLanguage())
          {
            LanguageDropdown.value = i;
          }
        }
      }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Changes the language and reloads the scene
    public void SetLanguage()
    {
      if(GlobalGameHandler.GetCurrentLanguage() != languages[LanguageDropdown.value]){
        GlobalGameHandler.ChangeLanguage(languages[LanguageDropdown.value]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = isFullscreen;
    }

    //Toggle on/off the directional arrow
    public void ChangeArrow()
    {
      if(!GlobalGameHandler.PlayerWantsDirectionalArrow() && ToggleCheckBox.isOn){
        GlobalGameHandler.ToggleDirectionalArrowSetting();
      } else if (GlobalGameHandler.PlayerWantsDirectionalArrow() && !ToggleCheckBox.isOn){
        GlobalGameHandler.ToggleDirectionalArrowSetting();
      }
    }

    public void ResetGame()
    {
      AreYouSure.SetActive(true);
    }

    public void ResetYes()
    {
        PlayerPrefs.DeleteAll();
        GameObject.Find("GlobalGameHandler").GetComponent<GlobalGameHandler>().Awake();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetNo()
    {
      AreYouSure.SetActive(false);
    }

    public void CloseOptions()
    {
      if(AreYouSure)
      {
        AreYouSure.SetActive(false);
      }
      optionMenu.SetActive(false);
    }

}
