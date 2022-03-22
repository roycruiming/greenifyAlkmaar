using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleDynaScript : MonoBehaviour
{
    public Camera puzzleCam;

    public GameObject player;

    public GameObject recycleSprite;
    public GameObject gasSprite;
    public GameObject factorySprite;
    public GameObject windmillSprite;
    public GameObject solarSprite;

    public Button option1, option2, option3, option4;

    protected Camera activeCamera;

    // Start is called before the first frame update
    void Start()
    {
        ActivatePuzzle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActivatePuzzle()
    {
        InitiatalizeCam();

        GameObject[] mainSpriteList = createSpriteList();

        InitializeButtons(CalculateAnswer(mainSpriteList));

        StartCoroutine(loadSequence(mainSpriteList));

    }

    //CAMERAS
    void InitiatalizeCam()
    {
        player.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        activeCamera = Instantiate(puzzleCam, new Vector3(245f, 300f, -545f), Quaternion.identity);
        activeCamera.transform.SetParent(transform, false);
        transform.GetComponent<Canvas>().worldCamera = activeCamera;
    }

    IEnumerator LeaveCam()
    {
        Debug.Log("hi");
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(activeCamera);
    }

    //BUTTONS
    void InitializeButtons(int answer)
    {
        Dictionary<Button, int> buttonValues = new Dictionary<Button, int>();

        buttonValues.Add(option1, 6);
        buttonValues.Add(option2, 6);
        buttonValues.Add(option3, 6);
        buttonValues.Add(option4, 6);

        //Initialize the checks for doubles
        bool option1Doubles = false;
        bool option2Doubles = false;
        bool option3Doubles = false;


        //Check if any option has gained the right answer AND if none of them are doubles
        while ((buttonValues[option1] != answer && buttonValues[option2] != answer && buttonValues[option3] != answer && buttonValues[option4] != answer) || option1Doubles || option2Doubles || option3Doubles) {


            //try a certain combination
            buttonValues[option1] = Random.Range(0, 5);
            buttonValues[option2] = Random.Range(0, 5);
            buttonValues[option3] = Random.Range(0, 5);
            buttonValues[option4] = Random.Range(0, 5);

            //Refresh the double checks for the loop
            option1Doubles = buttonValues[option1] == buttonValues[option2] || buttonValues[option1] == buttonValues[option3] || buttonValues[option1] == buttonValues[option4];
            
            option2Doubles = buttonValues[option2] == buttonValues[option3] || buttonValues[option2] == buttonValues[option4];

            option3Doubles = buttonValues[option3] == buttonValues[option4];

        }

        Button correctButton = option1;

        foreach(KeyValuePair<Button, int> option in buttonValues)
        {
            if (option.Value == answer)
            {
                correctButton = option.Key;
            }
        }

        CreateButtons(buttonValues, correctButton);

    }

    void CreateButtons(Dictionary<Button, int> buttons, Button correctButton)
    {
        option1.transform.GetChild(0).GetComponent<Text>().text = buttons[option1].ToString();
        option1.onClick.AddListener(() => OptionClicked(correctButton, option1));

        option2.transform.GetChild(0).GetComponent<Text>().text = buttons[option2].ToString();
        option2.onClick.AddListener(() => OptionClicked(correctButton, option2));

        option3.transform.GetChild(0).GetComponent<Text>().text = buttons[option3].ToString();
        option3.onClick.AddListener(() => OptionClicked(correctButton, option3));
        
        option4.transform.GetChild(0).GetComponent<Text>().text = buttons[option4].ToString();
        option4.onClick.AddListener(() => OptionClicked(correctButton, option4));
    }

    void OptionClicked(Button answer, Button option)
    {
        if (answer == option)
        {
            GameObject.Find("QuestionText").GetComponent<Text>().text = "That is correct! Congratulations!";
            StartCoroutine(LeaveCam());
            //Success get thing!
        } else
        {
            GameObject.Find("QuestionText").GetComponent<Text>().text = "That is sadly incorrect, but please try again!";
            StartCoroutine(LeaveCam());
        }
    }
    
    int CalculateAnswer(GameObject[] spriteList)
    {
        int sustainableAmount = 0;

        foreach(GameObject sprite in spriteList)
        {
            if (sprite.GetComponent<DynamicImageController>().energy == EnergyType.Sustainable)
            {
                sustainableAmount += 1;
            }
        }

        return sustainableAmount;
    }
    
    //ICONS
    IEnumerator loadSequence(GameObject[] listToShow)
    {
        
        createImage(listToShow[0]);
        yield return new WaitForSeconds(1f);
        createImage(listToShow[1]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[2]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[3]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[4]);
    }

    GameObject[] createSpriteList()
    {
        GameObject[] spriteList = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            switch(Random.Range(1, 6))
            {
                case 1:
                    spriteList[i] = windmillSprite;
                    break;
                case 2:
                    spriteList[i] = solarSprite;
                    break;
                case 3:
                    spriteList[i] = gasSprite;
                    break;
                case 4:
                    spriteList[i] = recycleSprite;
                    break;
                case 5:
                    spriteList[i] = factorySprite;
                    break;
            }
        }

        return spriteList;
    }

    void createImage(GameObject spriteToSpawn)
    {
        GameObject testImage = Instantiate(spriteToSpawn, new Vector3(105, 0, 0), Quaternion.identity) as GameObject;
        
        testImage.transform.SetParent(transform, false);
        testImage.transform.SetSiblingIndex(4);
    }
}


public enum EnergyType
{
    Sustainable, Limited
}
