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

    private GameObject cutsceneParent;
    private GameObject mainCamera;
    public List<GameObject> allPhaseObjects { get; set; }

    private List<List<GameObject>> allPhaseObjectsList = new List<List<GameObject>>();

    public void Awake() {
        GameObject.Find("splash").GetComponent<FadeInOutScript>().MakeInVisible();
        
        initLevel();
        
        StartCoroutine(showcaseIntroCutscene());


        //intro cutscene


    }

    public void initLevel()
    {
        this.mainCamera = GameObject.Find("Main Camera");
        this.cutsceneParent = GameObject.Find("cutscenesHolder");
    }

    IEnumerator showcaseIntroCutscene() {
        
        this.mainCamera.SetActive(false);
        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        //GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("intro de meent"));
        yield return new WaitForSeconds(18);

        this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(false);
        this.mainCamera.SetActive(true);
        yield return null;

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

    private void SwitchCamera(GameObject cameraToEnable, GameObject cameraToDisable) {
        if(cameraToDisable != null) cameraToDisable.SetActive(false);
        if(cameraToEnable != null) cameraToEnable.SetActive(true);
        
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
