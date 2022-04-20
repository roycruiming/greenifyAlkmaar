using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDetectionObject : MonoBehaviour
{
    public bool treeFallAction;
    public bool arrowExplanation; 
    public bool generalHelperExplanation;

    private void OnCollisionEnter(Collision other) {
        TutorialLevel tutorialHandler = GameObject.Find("TutorialLevelHandler").GetComponent<TutorialLevel>();

        if(other.gameObject.name == "C_man_1_FBX2013" || other.gameObject.name == "3RD Person") {
            
            if(treeFallAction) tutorialHandler.triggerTreeFallingDown();
            if(arrowExplanation) tutorialHandler.showcaseTutorialMessage();
            if(generalHelperExplanation) tutorialHandler.showcaseGeneralHelperExplanation();
        }
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
