using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleDynaScript : MonoBehaviour
{
    public Camera puzzleCam;

    public GameObject player;

    public GameObject recycleSprite;
    public GameObject gasSprite;
    public GameObject factorySprite;
    public GameObject windmillSprite;
    public GameObject solarSprite;

    public int valueTest;

    public DirectionalArrow arrow;

    public Button option1, option2, option3, option4;

    

    protected Camera activeCamera;

    protected IEnumerator loadCoroutine;


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

        InitiatalizeCam();

        transform.GetChild(1).GetComponent<Text>().text = "How many Sustainable Energy solutions did you see?";

        GameObject[] mainSpriteList = createSpriteList();

        InitializeButtons(CalculateAnswer(mainSpriteList));

        loadCoroutine = loadSequence(mainSpriteList);
        StartCoroutine(loadCoroutine);

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

    IEnumerator LeaveCam(bool cleared)
    {
        StopCoroutine(loadCoroutine);
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Color clear = new Color(0.05f,0.9f,0f);
        
        gameObject.transform.parent.GetComponent<Renderer>().material.color = clear;

        Destroy(this.gameObject);
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
            buttonValues[option1] = Random.Range(0, 6);
            buttonValues[option2] = Random.Range(0, 6);
            buttonValues[option3] = Random.Range(0, 6);
            buttonValues[option4] = Random.Range(0, 6);

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

    void PuzzleVictory() {
        switch (transform.parent.name)
        {
            case "Cube 0":
                GameObject doos = GameObject.Find("Cube 0");
                GameObject rook = doos.transform.Find("Smoke").gameObject;
                rook.SetActive(false);  //fix doos 
                break;
            case "Cube 1": // spawn solar panel, highlight positie, spawn een ster wanneer gedaan
                print("");
                break;
            case "Cube 2": // spawn solar panel, highlight positie, spawn een ster wanneer gedaan
                print("");
                break;
            case "Cube 3": // laat windmolen draaien 
                {
                    GameObject obj = GameObject.Find("Cube 3");
                    GameObject child = obj.transform.Find("Smoke").gameObject;
                    child.SetActive(false);

                    GameObject mill = GameObject.Find("Windmill (3)");
                    GameObject wiek = mill.transform.Find("Wiek").gameObject;
                    wiek.GetComponent<AnimationsScript>().isAnimated = true;

                    GameObject mill2 = GameObject.Find("Windmill (4)");
                    GameObject wiek2 = mill2.transform.Find("Wiek").gameObject;
                    wiek2.GetComponent<AnimationsScript>().isAnimated = true;

                    GameObject mill3 = GameObject.Find("Windmill (5)");
                    GameObject wiek3 = mill3.transform.Find("Wiek").gameObject;
                    wiek3.GetComponent<AnimationsScript>().isAnimated = true;

                    GameObject mill4 = GameObject.Find("Windmill (6)");
                    GameObject wiek4 = mill4.transform.Find("Wiek").gameObject;
                    wiek4.GetComponent<AnimationsScript>().isAnimated = true;



                }
                break;
            case "Cube 4":
                { // laat windmolen draaien 
                    GameObject obj2 = GameObject.Find("Cube 4");
                    GameObject child2 = obj2.transform.Find("Smoke").gameObject;
                    child2.SetActive(false);

                    GameObject mill5 = GameObject.Find("Windmill (1)");
                    GameObject wiek5 = mill5.transform.Find("Wiek").gameObject;
                    wiek5.GetComponent<AnimationsScript>().isAnimated = true;

                    GameObject mill6 = GameObject.Find("Windmill (2)");
                    GameObject wiek6 = mill6.transform.Find("Wiek").gameObject;
                    wiek6.GetComponent<AnimationsScript>().isAnimated = true;



                }
                break;
            default:
                print("something went very wrong");
                break;

        }
    }

    void OptionClicked(Button answer, Button option)
    {
        if (answer == option)
        {
            transform.GetChild(1).GetComponent<Text>().text = "That is correct! Congratulations!";

            arrow.DeleteItemInList(valueTest);

            PuzzleVictory(); 

            StartCoroutine(LeaveCam(true));
            //Success get thing!

            

        } else
        {
            transform.GetChild(1).GetComponent<Text>().text = "That is sadly incorrect, but please try again!";
            StartCoroutine(LeaveCam(false));
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
        yield return new WaitForSeconds(2f);
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

    void ClearPuzzle()
    {

    }
}


public enum EnergyType
{
    Sustainable, Limited
}
