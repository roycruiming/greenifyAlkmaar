using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;


public class UnlockableUiButton : MonoBehaviour
{
    public Unlockable unlockAble = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void SetUnlockable(Unlockable unlockable) {
    //     this.unlockAble = unlockable;
    // }

    public void PurchaseOrEquipUnlockable() {
        //check if unlockable is Character or Power Up
        if(unlockAble.isUnlocked == true && GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>() != null) {
            if(unlockAble.type == UnlockableType.character) {
                if(unlockAble.isPurchased == false) {
                    //try to purchase the object
                    if(GlobalGameHandler.GetTotalPlayerCointsAmount() >= unlockAble.price) {
                        //is able to purchase the object so purchase it
                        GlobalGameHandler.LowerPlayerCoints((int)Math.Ceiling(unlockAble.price));
                        unlockAble.isPurchased = true;
                        unlockAble.UpdateInfoToDisk();
                    }
                    else GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>().showcasePopUpMessage(GlobalGameHandler.GetTextByDictionaryKey("not enough coints"),4f);

                    //update total coints of the player in the progression store
                    ProgressionStoreHandler progressionStoreHandler = GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>();
                    if(progressionStoreHandler != null) {
                        progressionStoreHandler.UpdateTotalPlayerCointsUI();
                        progressionStoreHandler.updateShowcase(unlockAble);
                    }
                }
                else {
                    GlobalGameHandler.ChangeSelectedCharacterByUnlockId(unlockAble.id); //equip the character
                    GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>().showcasePopUpMessage(GlobalGameHandler.GetTextByDictionaryKey("succesfully equiped character"),4f);
                } 
            }
        }
        else GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>().showcasePopUpMessage(GlobalGameHandler.GetTextByDictionaryKey("unlockable not unlocked yet") + unlockAble.unlockedInLevel,4f);
    }
}
