using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    private float popUpDuration;

    GameObject HudCanvas;
    GameObject PopUpMessageContainer;



    //constructor
    void Awake()
    {
        this.popUpDuration = 8f; //seconds

        this.HudCanvas = GameObject.FindWithTag("HUDCanvas"); //find the hudcanvas is in the current scene
        this.PopUpMessageContainer = HudCanvas.transform.Find("PopUpMessageContainer").gameObject; //get the popupmessage container from the hudcanvas
        this.PopUpMessageContainer.SetActive(false); //disable showing the pop up mesasge container when the scene is initaited
    }

    public void ShowcaseMessage(string messageText) {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
