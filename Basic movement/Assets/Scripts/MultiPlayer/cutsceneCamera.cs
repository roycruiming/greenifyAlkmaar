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


    }

    // Update is called once per frame
    void Update()
    {


    }
}
