using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Dropdown LanguageDropdown;
    public GameObject optionMenu;
    public Toggle ToggleCheckBox;

    public string LanguageSelected;
    int LanguageSelectedIndex;
    List<string> languages;

    //zet de juiste values in dropdown
    void Start()
    {
      ToggleCheckBox = transform.Find("ArrowToggle").GetComponent<Toggle>();
      if(ToggleCheckBox){
        if(GlobalGameHandler.PlayerWantsDirectionalArrow()){
          ToggleCheckBox.isOn = true;
        } else {
          ToggleCheckBox.isOn = false;
        }
      }

      languages = GlobalGameHandler.GetLanguagesList();
      LanguageDropdown.ClearOptions();
      LanguageDropdown.AddOptions(languages);

      for (int i = 0; i < languages.Count; i++)
      {
        if(languages[i] == GlobalGameHandler.GetCurrentLanguage())
        {
          LanguageDropdown.value = i;
        }
      }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetLanguage()
    {
      Debug.Log(languages[LanguageDropdown.value]);
      GlobalGameHandler.ChangeLanguage(languages[LanguageDropdown.value]);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeArrow()
    {
      if(!GlobalGameHandler.PlayerWantsDirectionalArrow() && ToggleCheckBox.isOn){
        GlobalGameHandler.ToggleDirectionalArrowSetting();
      } else if (GlobalGameHandler.PlayerWantsDirectionalArrow() && !ToggleCheckBox.isOn){
        GlobalGameHandler.ToggleDirectionalArrowSetting();
      }
    }

    public void CloseOptions()
    {
        optionMenu.SetActive(false);
    }

}
