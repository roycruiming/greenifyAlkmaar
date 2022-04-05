using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSCene : MonoBehaviour
{
    public ObjectivesController objCon;
    public GameObject cutscene;
    public GameObject MainCamera;

    private Transform[] children;
    private bool timerBool = false;
    private bool messageDisplay = false;


    private void Start()
    {
        objCon = FindObjectOfType<ObjectivesController>();
    }

    // Update is called once per frame
    void Update()
    {
        //start cutscene
        MainCamera.gameObject.SetActive(false);
        children = gameObject.GetComponentsInChildren<Transform>();
        objCon.GameTimer.gameObject.SetActive(false);
        objCon.TextUiCounter.gameObject.SetActive(false);
        Object.Destroy(cutscene, 2.0f);

        if (Input.GetKey("p"))
        {
            Object.Destroy(cutscene);

        }

        if (messageDisplay == false)
        {
            messageDisplay = true;
            if (GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>() != null)
            {
                GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("back"));
            }
        }


        if (children.Length == 1 )
        {
            if(timerBool == false)
            {
                objCon.SetTimerToNul();
                timerBool = true;
            }
            else
            {
                MainCamera.gameObject.SetActive(true);
                objCon.GameTimer.gameObject.SetActive(true);
                objCon.TextUiCounter.gameObject.SetActive(true);
            }



        }

    }
}
