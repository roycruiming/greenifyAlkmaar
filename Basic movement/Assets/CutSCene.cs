using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSCene : MonoBehaviour
{

    public GameObject cutscene;
    public GameObject MainCamera;
    public DirectionalArrow arrow;

    private Transform[] children;
    private bool timerBool = false;
    private bool messageDisplay = false;




    // Update is called once per frame
    void Update()
    {




        MainCamera.gameObject.SetActive(false);
        Object.Destroy(cutscene, 15.0f);
        children = gameObject.GetComponentsInChildren<Transform>();
        arrow.GameTimer.gameObject.SetActive(false);
        arrow.TextUiCounter.gameObject.SetActive(false);

        if(messageDisplay == false)
        {
            messageDisplay = true;
            if (GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>() != null)
            {
                GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null, null, new List<string> {
                "testeeadsasdasdasd",
                "sadsadasdsaasddad dasdasdasd dadsdad"
                });
            }
        }
       

        if (children.Length == 1 )
        {
            if(timerBool == false)
            {
                arrow.SetTimerToNul();
                timerBool = true;
            }
            else
            {
                MainCamera.gameObject.SetActive(true);
                arrow.GameTimer.gameObject.SetActive(true);
                arrow.TextUiCounter.gameObject.SetActive(true);
            }
            


        }
        
    }
}
