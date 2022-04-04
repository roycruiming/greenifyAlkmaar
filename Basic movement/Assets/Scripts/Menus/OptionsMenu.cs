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

    public string LanguageSelected;
    int LanguageSelectedIndex;
    List<string> languages;


    void Start()
    {
      languages = GlobalGameHandler.GetLanguagesList();
      LanguageDropdown.ClearOptions();
      LanguageDropdown.AddOptions(languages);
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

    public void CloseOptions()
    {
        optionMenu.SetActive(false);
    }

}
