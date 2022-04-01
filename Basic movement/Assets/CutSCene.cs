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
        //start cutscene
        MainCamera.gameObject.SetActive(false);
        children = gameObject.GetComponentsInChildren<Transform>();
        arrow.GameTimer.gameObject.SetActive(false);
        arrow.TextUiCounter.gameObject.SetActive(false);
        Object.Destroy(cutscene, 2.0f);


        if (messageDisplay == false)
        {
            messageDisplay = true;
            if (GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>() != null)
            {
                /*GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null, null, new List<string> {
                "Welkom in Alkmaar, we zijn hier bij de meent. Deze ijsbaan heeft 940 zonnepanelen en deze hebben een opbrengst van 300.000 kwh (kilowatt uur) per jaar.",
                "Dit staat gelijk aan het stroomverbruik van 100 huishoudens. Echter werken de zonnepanelen nu niet, zoek rond in het level naar oplossingen! ",
                "Veel succes!"
                });*/
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
