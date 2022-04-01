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
    public List<string> languages;


    void Start()
    {
      //LanguageSelected = GlobalGameHandler.GetInstance().currentLanguage.ToString();
      //Debug.Log(LanguageSelected);
      //LanguageDropdown.ClearOptions();

    //  for (int i = 0; i < languages.Count; i++)
    //  {
    //    if(languages[i] == LanguageSelected)
    //    {
    //      LanguageSelectedIndex = i;
    //    }
    //  }
    //  LanguageDropdown.AddOptions(languages);
    //  LanguageDropdown.value = LanguageSelectedIndex;
    //  LanguageDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetLanguage(int WantedLanguage)
    {
      Debug.Log(languages[WantedLanguage]);
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
