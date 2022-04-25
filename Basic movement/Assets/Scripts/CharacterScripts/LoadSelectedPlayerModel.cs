using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelectedPlayerModel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int selectedCharacterUnlockableId = PlayerPrefs.GetInt("ActiveCharacterUnlockId");

        if(selectedCharacterUnlockableId != -1) {
            foreach(Unlockable ue in GlobalGameHandler.GetAllUnlockablesInfoByType(UnlockableType.character)) {
                if(ue.id == selectedCharacterUnlockableId) {

                    transform.Find("man_casual").gameObject.SetActive(false); //disable current shown character
                    this.transform.Find(ue.GetPolyPerfectCharacterName()).gameObject.SetActive(true);
                }
            }
        }

        // Unlockable unlockableCharacter = this.allCharacterUnlockablesInfo[indexCharacterList];
        // GameObject charactersShowcaseContainer = GameObject.Find("CharacterShowCaseContainer");

        // int childrenCount = charactersShowcaseContainer.transform.childCount-1;
        // if (charactersShowcaseContainer != null)
        // {
        //     for (int i = 0; i < childrenCount; i++) charactersShowcaseContainer.transform.GetChild(i).gameObject.SetActive(false); //disable current shown character


        //     charactersShowcaseContainer.transform.Find(unlockableCharacter.polyPerfectModelName).gameObject.SetActive(true);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
