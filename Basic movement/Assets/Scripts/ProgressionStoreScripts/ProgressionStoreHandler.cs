using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressionStoreHandler : MonoBehaviour
{
    List<Unlockable> allUnlockablesInfo;
    List<Unlockable> allCharacterUnlockablesInfo;

    private int currentCharacterShopIndex;
    private int currentPowerUpShopIndex;
    private int currentlySelectedCharacterShopId;
    private int currentlySelectedPowerUpShopId;
    // Start is called before the first frame update
    void Start()
    {
        this.allUnlockablesInfo = GlobalGameHandler.GetAllUnlockablesInfo();
        this.allCharacterUnlockablesInfo = GlobalGameHandler.GetAllUnlockablesInfoByType(UnlockableType.character);
        
        this.UpdateTotalPlayerCointsUI();
    }

    private void SetFirstUnlocksDataAndIndex() {
        this.currentCharacterShopIndex = 0;
        this.currentPowerUpShopIndex = 0;

        updateShowcase(this.allCharacterUnlockablesInfo[currentCharacterShopIndex]);
    }

    public void showcaseNextCharacter() {
        if(currentCharacterShopIndex + 1 < allCharacterUnlockablesInfo.Count) {
            this.currentCharacterShopIndex++;
            updateShowcase(allCharacterUnlockablesInfo[currentCharacterShopIndex]);
        }
    }

    public void showcasePreviousCharacter() {
        if(currentCharacterShopIndex - 1 >= 0) {
            this.currentCharacterShopIndex--;
            updateShowcase(allCharacterUnlockablesInfo[currentCharacterShopIndex]);
        }
    }

    public void showcaseNextPowerUp() {

    }

    public void showcasePreviousPowerUp() {

    }

    private void updateShowcase(Unlockable unlockInfo) {
        GameObject containerElement = null;
        if(unlockInfo.type == UnlockableType.character) containerElement = GameObject.Find("Characters_Showcase_Container");
        else if(unlockInfo.type == UnlockableType.powerUp) containerElement = GameObject.Find("PowerUps_Showcase_Container");

        if(containerElement != null) {
            //always set the price, let the purchased info overwrite if needed
            containerElement.transform.Find("coints_amount_text").GetComponent<TextMeshProUGUI>().text = unlockInfo.price.ToString();
            containerElement.transform.Find("level_unlock_text").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey("unlocked in level") + " " + unlockInfo.unlockedInLevel.ToString();
            containerElement.transform.Find("showcase_image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Unlock_Images/" + unlockInfo.exampleImageName);

            if(unlockInfo.isPurchased) {
                containerElement.transform.Find("coints_amount_text").GetComponent<TextMeshProUGUI>().text = GlobalGameHandler.GetTextByDictionaryKey("purchased");
                containerElement.transform.Find("lock-icon").gameObject.SetActive(false); 
            }

            if(unlockInfo.isUnlocked) {
                containerElement.transform.Find("lock-icon").gameObject.SetActive(true);
            }
        }
    }

    private void UpdateTotalPlayerCointsUI() {
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
