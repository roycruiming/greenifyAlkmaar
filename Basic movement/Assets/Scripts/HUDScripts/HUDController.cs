using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    private double nextUpdate = 0.2;
    private float currentPopUpTime = 0f;

    private float nextMessageWaitingTime = 2f; //in seconds
    private float hidePopUpGroupWaitingTime = 4f;

    private char[] toBePrintedCharacters;
    private char[] currentlyPrintedCharacters;

    private double messageBoxMaxChars = 160;

    private List<string> messageSequence;

    public GameObject HudCanvas; //find the hudcanvas is in the current scene
    public GameObject PopUpMessageContainer; //get the popupmessage container from the hudcanvas

    private InformationHelper currentInformationHelper;
    private bool hidingPopUpContainer = false;

    private bool cancelHidingProgress = false;

    public void Awake() {
        this.HudCanvas = GameObject.FindWithTag("HUDCanvas");
        this.PopUpMessageContainer = HudCanvas.transform.Find("PopUpMessageContainer").gameObject;
        this.messageSequence = null;
        this.currentInformationHelper = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PrintTypeMessage",1f,0.08f); //initaite the script to type each message letter by letter if it is set. 0.08 means the interval between typing each letter
    }

    public void ShowcaseMessage(string messageText, InformationHelper senderInfo = null, List<string> senderMessageSequence = null) {
        bool useMessageText = false;
        this.cancelHidingProgress = true;

        this.clearMessageSequence(); //clear old message sequence if it exists
 
        if(senderMessageSequence != null && senderMessageSequence.Count > 0) {
            //multiple messages have to be shown
            //set the first messageText as the messageSequence, so ignore initial Message text
            messageText = senderMessageSequence[0];
            useMessageText = true;
            senderMessageSequence.RemoveAt(0);
            //check if message sequence still contains messages after displaying and removing it from the list
            if(senderMessageSequence.Count > 0) {
                this.messageSequence = senderMessageSequence;
            }
        }


        if(this.HudCanvas != null && this.PopUpMessageContainer != null) {
            if(this.PopUpMessageContainer.activeSelf == false) this.PopUpMessageContainer.SetActive(true); //displays the popupmessage container
            this.hidingPopUpContainer = false;

            //two options for this function, one is that it just displays a message with the mascot in the screen
            //other option is that the game object is being readed and if it contains an sprite display this sprite also
            if(senderInfo == null) {
                //just a message is going to be displayed with the default mascotte icon and PopUpImage should be hidden
                this.PopUpMessageContainer.transform.Find("PopUpCharacterIcon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/mascotte");
                this.PopUpMessageContainer.transform.Find("PopUpImage").gameObject.SetActive(false);
                
                this.setCharacterArrays(messageText);
            }
            else {
                this.currentInformationHelper = senderInfo;
                if(useMessageText == true) this.setCharacterArrays(messageText); //use the senderMessageSequence text instead of sender.info text
                else this.setCharacterArrays(GlobalGameHandler.GetTextByDictionaryKey(senderInfo.informationTextDictionaryKey)); //set text
                
                //show sprite if it is set
                if(senderInfo.spriteToShow != null) {
                    GameObject spriteElement = this.PopUpMessageContainer.transform.Find("PopUpImage").gameObject;
                    spriteElement.SetActive(true); //show image UI element
                    spriteElement.GetComponent<Image>().sprite = senderInfo.spriteToShow; //set the sprite image
                }
                else this.PopUpMessageContainer.transform.Find("PopUpImage").gameObject.SetActive(false);
                //change icon of the character who is talking the message
                if(senderInfo.characterIcon != null) this.PopUpMessageContainer.transform.Find("PopUpCharacterIcon").gameObject.GetComponent<Image>().sprite = senderInfo.characterIcon;
                else this.PopUpMessageContainer.transform.Find("PopUpCharacterIcon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/mascotte");
            }

            //if the message contains alot of characters
            this.calculateMessageFontSize(this.toBePrintedCharacters.GetLength(0));

            //calculateExtraReadingTime();
        }
    }

    public void SimulateUnlock() {
        if(this.HudCanvas.transform.Find("UnlocksContainer") != null) {
            GameObject unlocksContainer = this.HudCanvas.transform.Find("UnlocksContainer").gameObject;

            if(unlocksContainer.activeSelf == false) unlocksContainer.SetActive(true);

            unlocksContainer.transform.Find("unlock_1").GetComponent<FadeInOutScript>().StartFading();
            unlocksContainer.transform.Find("unlock_2").GetComponent<FadeInOutScript>().StartFading();

            unlocksContainer.transform.Find("unlock_2").transform.Find("Coints_Amount").GetComponent<UnityEngine.UI.Text>().text = "220";
        }



    }

    private void calculateMessageFontSize(int characters) {
        //160 is the basis of what fits with fontsize 36
        //Debug.Log("Chars count: " + characters);
        if(characters < this.messageBoxMaxChars) this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().fontSize = 36;
        else if(characters > this.messageBoxMaxChars && characters <= 240) this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().fontSize = 26;
        else if(characters > this.messageBoxMaxChars && characters <= 300) this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().fontSize = 22;
    }

    private void calculateExtraReadingTime() {
        //characters that fit the message box container +/- = 160
        int messageTotalCharCount = this.toBePrintedCharacters.GetLength(0);

        //if characters are 80 or above add 2 extra seconds reading time
        if(messageTotalCharCount >= this.messageBoxMaxChars / 2) { 
            this.hidePopUpGroupWaitingTime = this.hidePopUpGroupWaitingTime + 2f; //bugged keeps setting it to the infinite
            this.nextMessageWaitingTime = this.nextMessageWaitingTime + 1.2f;
        } 
    }

    private void clearMessageSequence() {
        this.messageSequence = null;
    }
    
    private void setCharacterArrays(string text) {
            //set the characters that have to be printed letter by letter
            this.toBePrintedCharacters = text.ToCharArray();
            this.currentlyPrintedCharacters = new char[this.toBePrintedCharacters.GetLength(0)];
            this.EmptyCurrentlyPrintedCharacters(); //refresh the currently pritned characters list
    }

    private void EmptyCurrentlyPrintedCharacters() {
        this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().text = ""; //empty the current text popup message
        for(int i = 0; i < this.currentlyPrintedCharacters.GetLength(0); i++) {
            currentlyPrintedCharacters[i] = ' ';
        }
    }

    private void PrintTypeMessage() {
        //check if the lists contain letters
        if(this.toBePrintedCharacters != null && this.currentlyPrintedCharacters != null) {
            //check if all characters have been printed
            if(this.AllCharactersHaveBeenPrinted() == false) {
                //print next character and add it to the currentlypritnedcharacters list
                for(int i = 0; i < this.toBePrintedCharacters.GetLength(0); i++) {
                    if(this.toBePrintedCharacters[i] != currentlyPrintedCharacters[i]) {
                        //set next text
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
                if(this.PopUpMessageContainer.activeSelf == true) { //popupmessage is still displayed so initiate the process to hide it or showcase the next message sequence
                    if(this.messageSequence != null && this.messageSequence.Count > 0)  StartCoroutine( NextPopUpMessage()); //show the next message from the message sequence
                    else if(hidingPopUpContainer == false) {
                        this.cancelHidingProgress = false;
                        StartCoroutine(HidePopUpMessage()); //hide the message container 
                    }
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

    IEnumerator NextPopUpMessage()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        this.toBePrintedCharacters = null;
        this.currentlyPrintedCharacters = null;
        hidingPopUpContainer = true; //make the script think it is hidden, other wise it will try to hide it while waiting
        yield return new WaitForSeconds(this.nextMessageWaitingTime);
        hidingPopUpContainer = false;
        this.ShowcaseMessage("",null,this.messageSequence);
    }

    IEnumerator HidePopUpMessage() //reset all necessary variables and hide the messagepopup hud element
    {
        hidingPopUpContainer = true;
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(this.hidePopUpGroupWaitingTime);
        if(cancelHidingProgress == false) {
            this.toBePrintedCharacters = new char[4];
            this.currentlyPrintedCharacters = new char[4];
            this.PopUpMessageContainer.SetActive(false); //hide popup message container

            //hide image block
            this.PopUpMessageContainer.transform.Find("PopUpImage").gameObject.SetActive(false);

            //remove later!!! hide temporary unlocks:
            //this.TemporaryUnlocksHide();

            //reset the sprite character icon to the default mascot
            this.PopUpMessageContainer.transform.Find("PopUpCharacterIcon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/mascotte");

            //reset the message sequence list
            this.messageSequence = null;

            //reset current information helper to null
            this.currentInformationHelper = null;

            //reset font size
            this.PopUpMessageContainer.transform.Find("PopUpText").gameObject.GetComponent<TextMeshProUGUI>().fontSize = 36;

            //reset waiting times for hiding and next message
            this.nextMessageWaitingTime = 2f; //default value is 2f
            this.hidePopUpGroupWaitingTime = 4f;

        }
        else this.cancelHidingProgress = false;
    }

    private void TemporaryUnlocksHide() {
            GameObject unlocksContainer = this.HudCanvas.transform.Find("UnlocksContainer").gameObject;
            unlocksContainer.transform.Find("unlock_1").GetComponent<FadeInOutScript>().StartFadingOut();
            unlocksContainer.transform.Find("unlock_2").GetComponent<FadeInOutScript>().StartFadingOut();
            unlocksContainer.transform.Find("unlock_2").transform.Find("Coints_Amount").GetComponent<UnityEngine.UI.Text>().text = "";
    }


}
