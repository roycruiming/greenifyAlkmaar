using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionStoreHandler : MonoBehaviour
{
    List<Unlockable> allUnlockablesInfo;
    List<Unlockable> allCharacterUnlockablesInfo;

    private int currentCharacterShopIndex;
    private int currentPowerUpShopIndex;
    private int currentlySelectedCharacterShopId;
    private int currentlySelectedPowerUpShopId;

    private bool popUpIsBeingShown = false;
    // Start is called before the first frame update
    void Start()
    {
        this.allUnlockablesInfo = GlobalGameHandler.GetAllUnlockablesInfo();
        this.allCharacterUnlockablesInfo = GlobalGameHandler.GetAllUnlockablesInfoByType(UnlockableType.character);
        
        this.UpdateTotalPlayerCointsUI();
        this.SetFirstUnlocksDataAndIndex();
        //updateShowcase(allUnlockablesInfo[0]);
    }

    private void SetFirstUnlocksDataAndIndex() {
        this.currentCharacterShopIndex = 0;
        this.currentPowerUpShopIndex = 0;

        updateShowcase(this.allCharacterUnlockablesInfo[currentCharacterShopIndex]); //set characters
        //function for setting the first powerup
    }

    public void showcaseNextCharacter() {
        if(currentCharacterShopIndex + 1 < allCharacterUnlockablesInfo.Count) {
            this.currentCharacterShopIndex++;
            updateShowcase(allCharacterUnlockablesInfo[currentCharacterShopIndex]);
            changeShowcaseCharacter(currentCharacterShopIndex);
        }
    }

    public void LeaveProgressionStore() {

        SceneManager.LoadScene("MainMenu");
    }

    private void changeShowcaseCharacter(int indexCharacterList) {
        Unlockable unlockableCharacter = this.allCharacterUnlockablesInfo[indexCharacterList];
        GameObject charactersShowcaseContainer = GameObject.Find("CharacterShowCaseContainer");

        int childrenCount = charactersShowcaseContainer.transform.childCount-1;
        if (charactersShowcaseContainer != null)
        {
            for (int i = 0; i < childrenCount; i++) charactersShowcaseContainer.transform.GetChild(i).gameObject.SetActive(false); //disable current shown character


            if(string.IsNullOrEmpty(unlockableCharacter.GetPolyPerfectCharacterName()) == false) charactersShowcaseContainer.transform.Find(unlockableCharacter.GetPolyPerfectCharacterName()).gameObject.SetActive(true);
        }


    }

    public void showcasePreviousCharacter() {
        if(currentCharacterShopIndex - 1 >= 0) {
            this.currentCharacterShopIndex--;
            updateShowcase(allCharacterUnlockablesInfo[currentCharacterShopIndex]);
            changeShowcaseCharacter(currentCharacterShopIndex);
        }
    }

    public void showcaseNextPowerUp() {

    }

    public void showcasePreviousPowerUp() {

    }

    public void updateShowcase(Unlockable unlockInfo) {
        GameObject containerElement = null, buyButton = null;
        if(unlockInfo.type == UnlockableType.character) {
            containerElement = GameObject.Find("Characters_Showcase_Container");
            buyButton = GameObject.Find("purchase_Char_But");
        }
        else if(unlockInfo.type == UnlockableType.powerUp) {
            containerElement = GameObject.Find("PowerUps_Showcase_Container");
            buyButton = GameObject.Find("purchase_PowerUp_But");
        }
        
        if(containerElement != null) {
            //always set the price, let the purchased info overwrite if needed
            containerElement.transform.Find("coints_amount_text").GetComponent<TextMeshProUGUI>().text = unlockInfo.price.ToString();
            containerElement.transform.Find("level_unlock_text").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey("unlocked in level") + " " + unlockInfo.unlockedInLevel.ToString();
            containerElement.transform.Find("showcase_image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Unlock_Images/" + unlockInfo.exampleImageName);
            //always set the corresponding unlockable into the UI button script
            if(buyButton.GetComponent<UnlockableUiButton>() != null) buyButton.GetComponent<UnlockableUiButton>().unlockAble = unlockInfo;

            if(unlockInfo.isPurchased) {
                containerElement.transform.Find("coints_amount_text").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey("purchased");
                containerElement.transform.Find("lock-icon").gameObject.SetActive(false); 

                //change the buy button text to equip
                if(unlockInfo.type == UnlockableType.character) buyButton.transform.Find("Text").GetComponent<Text>().text = GlobalGameHandler.GetTextByDictionaryKey("select character");
                //ELSE SELECT POWERUP TEXT ADD LATER ON!!!!
            }
            else buyButton.transform.Find("Text").GetComponent<Text>().text = GlobalGameHandler.GetTextByDictionaryKey("buy");

            if(unlockInfo.isUnlocked) {
                containerElement.transform.Find("lock-icon").gameObject.SetActive(false);
            }
            else {
                containerElement.transform.Find("lock-icon").gameObject.SetActive(true);
            }
        }
    }

    public void showcasePopUpMessage(string text, float durationInSeconds) {
        GameObject progressionStorePopUpMessageContainer = GameObject.Find("ProgressionStoreUIContainer").transform.Find("ProgressionStorePopUpMessageContainer").gameObject;
        progressionStorePopUpMessageContainer.SetActive(true);
        if(popUpIsBeingShown == false) StartCoroutine(showcasePopupMessage(text, durationInSeconds));
    }

    IEnumerator showcasePopupMessage(string text, float duration) {
        popUpIsBeingShown = true;
        GameObject.Find("PopUpMessageBackground").GetComponent<FadeInOutScript>().StartFading();
        yield return new WaitForSeconds(0.22f);
        GameObject.Find("PopUpMessageText").GetComponent<TextMeshProUGUI>().text = text;
        yield return new WaitForSeconds(duration);
        GameObject.Find("PopUpMessageBackground").GetComponent<FadeInOutScript>().StartFadingOut();
        GameObject.Find("PopUpMessageBackground").GetComponent<FadeInOutScript>().StartFadingOut();
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("PopUpMessageText").GetComponent<TextMeshProUGUI>().text = "";
        popUpIsBeingShown = false;

    }

    public void UpdateTotalPlayerCointsUI() {
        GameObject.Find("playerCointsTotal").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTotalPlayerCointsAmount().ToString();
    }
    public void purchaseUnlockable(int unlockableId) {
        Debug.Log("Inside");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
