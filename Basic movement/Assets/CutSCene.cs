using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSCene : MonoBehaviour
{
    public ObjectivesController objCon;
    public GameObject cutscene;
    public GameObject MainCamera;
    public DirectionalArrow arrow;

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


        if (messageDisplay == false)
        {
            messageDisplay = true;
            if (GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>() != null)
            {
                GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null, null, new List<string> {
                "Welkom in Alkmaar, we zijn hier bij de meent. Deze ijsbaan heeft 940 zonnepanelen en deze hebben een opbrengst van 300.000 kwh (kilowatt uur) per jaar.",
                "Dit staat gelijk aan het stroomverbruik van 100 huishoudens. Echter werken de zonnepanelen nu niet, zoek rond in het level naar oplossingen! ",
                "Veel succes!"
                });
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
