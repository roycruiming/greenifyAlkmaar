using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaasmarktLevel : MonoBehaviour, LevelBasis
{
    public string levelName { get; set; }
    public int totalTasksCount { get; set; }
    public int completedTasksCount { get; set; }
    public bool hasPlayedBefore { get; set; }

    public int progressionPhase { get; set; }

    public List<GameObject> allPhaseObjects { get; set; }

    private List<List<GameObject>> allPhaseObjectsList = new List<List<GameObject>>();

    public void Awake() {
        GameObject.Find("splash").GetComponent<FadeInOutScript>().MakeInVisible();
    }

    public void showSplashEffectAndSound() {
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(((AudioClip)Resources.Load("Sounds/water_splash")));

        StartCoroutine(showcaseSplashEffect());
    }

    public IEnumerator showcaseSplashEffect() {
        GameObject.Find("splash").GetComponent<FadeInOutScript>().MakeVisible();
        yield return new WaitForSeconds(0.56f);

        GameObject.Find("splash").GetComponent<FadeInOutScript>().StartFadingOut();
        
    }

    public void initLevel()
    {
        throw new System.NotImplementedException();
    }

    public void saveProgress()
    {
        throw new System.NotImplementedException();
    }

    public void showcaseLevelProgression()
    {
        throw new System.NotImplementedException();
    }

    public void taskCompleted()
    {
        throw new System.NotImplementedException();
    }
}
