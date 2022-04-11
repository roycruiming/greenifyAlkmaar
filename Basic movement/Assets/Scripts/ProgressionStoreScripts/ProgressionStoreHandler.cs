using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressionStoreHandler : MonoBehaviour
{
    GameObject ProgressionStoreContainer;
    GameObject[] allCharacterUnlocksGameObjects;

    List<Unlockable> allUnlockablesInfo;
    // Start is called before the first frame update
    void Start()
    {
        this.ProgressionStoreContainer = GameObject.Find("ProgressionStoreUIContainer");
        this.allUnlockablesInfo = GlobalGameHandler.GetAllUnlockablesInfo();

        this.allCharacterUnlocksGameObjects = GameObject.FindGameObjectsWithTag("CharacterUnlockContainer");
        Debug.Log(allCharacterUnlocksGameObjects.GetLength(0) + " LENGTH");


        //set all the Unlockedable info into a visual presentation
        this.initCharactersUnlocksPresentation();

    }

    private void initCharactersUnlocksPresentation() {
        List<Unlockable> allCharacterUnlockables = GlobalGameHandler.GetAllUnlockablesInfoByType(UnlockableType.character);
        Debug.Log(allCharacterUnlockables[0] + " COUNT");
        for(int i = 0; i < allCharacterUnlockables.Count; i++) setCharacterUnlockableUiElement(allCharacterUnlocksGameObjects[i], allCharacterUnlockables[i]);
    }

    private void setCharacterUnlockableUiElement(GameObject UiCharUnlockContainer, Unlockable uInfo) {
        Debug.Log(UiCharUnlockContainer + "    " + uInfo);
        UiCharUnlockContainer.transform.Find("Price").GetComponent<TextMeshPro>().text = uInfo.price.ToString(); //allways set the price
        UiCharUnlockContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Unlock_Images/" + uInfo.exampleImageName);

        if(uInfo.isPurchased) {
            UiCharUnlockContainer.transform.Find("Checkmark-Icon").gameObject.SetActive(true);
            UiCharUnlockContainer.transform.Find("Coin-Icon").gameObject.SetActive(false);
            UiCharUnlockContainer.transform.Find("Lock-Icon").gameObject.SetActive(false);
            UiCharUnlockContainer.transform.Find("Unlocked-Overlay").gameObject.SetActive(true);
            UiCharUnlockContainer.transform.Find("Checkmark-Icon").gameObject.SetActive(true);
            UiCharUnlockContainer.transform.Find("Price").GetComponent<TextMeshPro>().text = GlobalGameHandler.GetTextByDictionaryKey("purchased");
        }

        if(uInfo.isUnlocked) {
            //is unlocked
            UiCharUnlockContainer.transform.Find("Lock-Text").gameObject.SetActive(true);
            UiCharUnlockContainer.transform.Find("Lock-Icon").gameObject.SetActive(false);
        }
        else {
            UiCharUnlockContainer.transform.Find("Lock-Text").GetComponent<TextMeshPro>().text = "Level " + uInfo.unlockedInLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
