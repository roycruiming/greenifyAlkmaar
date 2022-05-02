using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuessWordsAnswerButton : MonoBehaviour
{

    public char currentLetter;
    public int rowIndex;
    
    WordGuesserPuzzle wordGuesserController;
    
    float initialY;

    public void Awake() {
        initialY = this.transform.position.y;

        this.wordGuesserController = GameObject.Find("GuessWordsPuzzle").GetComponent<WordGuesserPuzzle>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { CommitLetterPress(); });
    }

    public void CommitLetterPress() {
        bool correctlySet = wordGuesserController.SetLetter(currentLetter);
        if(correctlySet) this.HideElement();
    }

    public void SetAndDisplayLetter(char letter) {
        this.currentLetter = letter;
        this.transform.Find("letter_display").GetComponent<TextMeshProUGUI>().text = letter.ToString();
    }

    public void HideElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 800f, this.transform.position.z);
    }

    public void ShowElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }
}
