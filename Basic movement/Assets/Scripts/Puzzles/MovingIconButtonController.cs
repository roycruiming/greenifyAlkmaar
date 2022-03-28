using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingIconButtonController : MonoBehaviour
{

    public Button option1, option2, option3, option4;

    public DirectionalArrow arrow;
    public int valueTest;

    public MovingIconCameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeButtons(int answer)
        //Creating a dictionary of buttons and the answer they display
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

        //TODO: this can be cleaner, probably by shuffling the number around instead of generating them constantly
        //Check if any option has gained the right answer AND if none of them are doubles
        while ((buttonValues[option1] != answer && buttonValues[option2] != answer && buttonValues[option3] != answer && buttonValues[option4] != answer) || option1Doubles || option2Doubles || option3Doubles)
        {


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

        //initialize correctbutton, option1 is meaningless here
        Button correctButton = option1;

        foreach (KeyValuePair<Button, int> option in buttonValues)
        {
            //if any button in the dictionary is the right answer, that will be the correctbutton now
            if (option.Value == answer)
            {
                correctButton = option.Key;
            }
        }

        CreateButtons(buttonValues, correctButton);

    }

    public void CreateButtons(Dictionary<Button, int> buttons, Button correctButton)
    {

        //TODO make this an actual foreach loop, should be easy
        //for each option in the list, show the number it's tied to, then set the onClick function
        option1.transform.GetChild(0).GetComponent<Text>().text = buttons[option1].ToString();
        option1.onClick.AddListener(() => OptionClicked(correctButton, option1));

        option2.transform.GetChild(0).GetComponent<Text>().text = buttons[option2].ToString();
        option2.onClick.AddListener(() => OptionClicked(correctButton, option2));

        option3.transform.GetChild(0).GetComponent<Text>().text = buttons[option3].ToString();
        option3.onClick.AddListener(() => OptionClicked(correctButton, option3));

        option4.transform.GetChild(0).GetComponent<Text>().text = buttons[option4].ToString();
        option4.onClick.AddListener(() => OptionClicked(correctButton, option4));
    }
    public void OptionClicked(Button answer, Button option)
    {
        //TODO disable the buttons when one is clicked, probably a seperate method


        if (answer == option)
        {
            //set the question text to the corresponding result
            transform.GetChild(1).GetComponent<Text>().text = "That is correct! Congratulations!";

            //arrow.objectivesCounter++;

            //The arrow's direction is dictated by what is in it's list, delete the matching target if it's done
            arrow.DeleteItemInList(valueTest);

            PuzzleVictory();

            //Start the process of changing back to the player
            StartCoroutine(cameraController.LeaveCam(true));



        }
        else
        {
            //set the question text to the corresponding result
            transform.GetChild(1).GetComponent<Text>().text = "That is sadly incorrect, but please try again!";
            //Start the process of changing back to the player
            StartCoroutine(cameraController.LeaveCam(false));
        }
    }
    //Script runs a different effect depending on the matching target, e.g. windmill 1 spinning or solar panel 2 spawning, etc.
    public void PuzzleVictory()
    {
        //By checking what the parent is, the right action can be taken
        switch (transform.parent.name)
        {
            //electrical box at the car parl
            case "Cube 0":
                GameObject doos = GameObject.Find("Cube 0");
                GameObject rook = doos.transform.Find("Smoke").gameObject;
                rook.SetActive(false);  //fix doos 
                break;
            //Solar panel in the housing district
            case "Cube 1": // spawn solar panel
                print("");
                break;
            //Solar panel at the tennis court
            case "Cube 2": // spawn solar panel
                print("");
                break;
            //Windmills at the other edge compared to the meent
            case "Cube 3": // start the animations on the winmill wings and delete the smoke
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
            //Windmills behind the Meent
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

    

}
