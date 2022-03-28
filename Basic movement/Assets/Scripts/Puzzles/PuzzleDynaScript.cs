using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleDynaScript : MonoBehaviour
{
    public MovingIconButtonController buttonController;
    public MovingIconCameraController cameraController;
    public MovingIconController iconController;





    


    public IEnumerator moverLoadCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatePuzzle()
    {
        //Start up the framing, e.g. the camera and the question
        cameraController.InitiatalizeCam();
        transform.GetChild(1).GetComponent<Text>().text = "How many Sustainable Energy solutions did you see?";

        //Create the actual sequence
        GameObject[] mainSpriteList = iconController.createSpriteList();

        //Create the buttons with the answer from the created spritelist, and assign the cameracontroller to the buttoncontroller so it can change on click
        buttonController.cameraController = cameraController;
        buttonController.InitializeButtons(CalculateAnswer(mainSpriteList));

        //Create and run the loading of the moving sprites
        moverLoadCoroutine = iconController.loadSequence(mainSpriteList);
        cameraController.moverLoadCoroutine = moverLoadCoroutine;
        StartCoroutine(moverLoadCoroutine);

    }


    int CalculateAnswer(GameObject[] spriteList)
    {
        // get the right answer by just, counting the icons with the right energytype
        int sustainableAmount = 0;

        foreach (GameObject sprite in spriteList)
        {
            if (sprite.GetComponent<MovingIconModel>().energy == EnergyType.Sustainable)
            {
                sustainableAmount += 1;
            }
        }

        return sustainableAmount;
    }


}


public enum EnergyType
{
    Sustainable, Limited
}
