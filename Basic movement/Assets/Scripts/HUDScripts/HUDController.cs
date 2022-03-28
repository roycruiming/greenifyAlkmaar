using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    private double nextUpdate = 0.2;
    private float currentPopUpTime = 0f;

    private char[] toBePrintedCharacters;
    private char[] currentlyPrintedCharacters;

    public GameObject HudCanvas; //find the hudcanvas is in the current scene
    public GameObject PopUpMessageContainer; //get the popupmessage container from the hudcanvas
    private bool hidingPopUpContainer = false;

    public void Awake() {
        this.HudCanvas = GameObject.FindWithTag("HUDCanvas");
        this.PopUpMessageContainer = HudCanvas.transform.Find("PopUpMessageContainer").gameObject;
    }

    public void ShowcaseMessage(string messageText) {
        if(this.HudCanvas != null && this.PopUpMessageContainer != null) {
            if(this.PopUpMessageContainer.activeSelf == false) this.PopUpMessageContainer.SetActive(true); //displays the popupmessage container
            this.hidingPopUpContainer = false;

            //set the characters that have to be printed letter by letter
            this.toBePrintedCharacters = messageText.ToCharArray();
            this.currentlyPrintedCharacters = new char[this.toBePrintedCharacters.GetLength(0)];
            this.EmptyCurrentlyPrintedCharacters(); //refresh the currently pritned characters list

            //set the pop up message text
            //this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().text = messageText;
        }
    }

    private void EmptyCurrentlyPrintedCharacters() {
        this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().text = ""; //empty the current text popup message
        for(int i = 0; i < this.currentlyPrintedCharacters.GetLength(0); i++) {
            currentlyPrintedCharacters[i] = ' ';
        }
    }


        // Update is called once per frame
    void Update()
    {
        
    }

    private void PrintTypeMessage() {
        //check if the lists contain letters
        if(this.toBePrintedCharacters != null && this.currentlyPrintedCharacters != null) {
            //check if all characters have been printed
            if(this.AllCharactersHaveBeenPrinted() == false) {
                //print next character and add it to the currentlypritnedcharacters list
                for(int i = 0; i < this.toBePrintedCharacters.GetLength(0); i++) {
                    if(this.toBePrintedCharacters[i] != currentlyPrintedCharacters[i]) {
                        this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().text = ""; //empty the current text popup message
                        currentlyPrintedCharacters[i] = toBePrintedCharacters[i]; //set the characters equal so the function knows this character is displayed
                        TextMeshProUGUI textMeshObject = this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>();
                        foreach(char c in currentlyPrintedCharacters) textMeshObject.text += c;
                        break; //break because the this function has to be called every second
                    }
                }
            }
            else {
                //initiate the timer to not display the popup message anymore
                if(this.PopUpMessageContainer.activeSelf == true) { //popupmessage is still displayed so initiate the process to hide it
                    if(hidingPopUpContainer == false) StartCoroutine(HidePopUpMessage());
                }
            }
        }
    }

    private bool AllCharactersHaveBeenPrinted() {
       for(int i = 0; i < this.toBePrintedCharacters.GetLength(0); i++) {
           if(this.toBePrintedCharacters[i] != this.currentlyPrintedCharacters[i]) return false; //not equal so return false
       }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PrintTypeMessage",1f,0.08f); //initaite the script to type each message letter by letter if it is set. 0.08 means the interval between typing each letter
    }

    IEnumerator HidePopUpMessage()
    {
        hidingPopUpContainer = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4);

        this.toBePrintedCharacters = new char[4];
        this.currentlyPrintedCharacters = new char[4];
        this.PopUpMessageContainer.SetActive(false); //hide popup message container
    }
}
