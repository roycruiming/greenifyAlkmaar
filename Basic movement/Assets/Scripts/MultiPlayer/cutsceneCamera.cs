using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneCamera : MonoBehaviour
{

    public GameObject CutsceneCamera;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Camera");
        CutsceneCamera = GameObject.Find("CutSceneCamera");

        MainCamera.SetActive(false);
        Destroy(CutsceneCamera, 6f);

        StartCoroutine(showcaseIntroCutscene());

    }

    IEnumerator showcaseIntroCutscene()
    {
        print("test");
        yield return new WaitForSeconds(1);
        GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null, null, GlobalGameHandler.GetSentencesByDictionaryKey("intro de meent"));
        yield return new WaitForSeconds(18);

        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
        if(CutsceneCamera == null)
        {
            MainCamera.SetActive(true);
        }

    }
}
