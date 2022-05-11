using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuessWordAnswerSpot : MonoBehaviour
{
    public int rowIndex;
    public char currentLetter = '-';
    public bool isCorrect = false;

    private float initialY;
    WordGuesserPuzzle wordGuesserController;

    // Start is called before the first frame update
    void Awake()
    {
        initialY = this.transform.position.y;
        this.wordGuesserController = GameObject.Find("GuessWordsPuzzle").GetComponent<WordGuesserPuzzle>();
    }

    public void setAndUpdateLetterDisplay(char letter, bool isPlacedCorrect) {
        this.currentLetter = letter;
        this.isCorrect = isPlacedCorrect;
        this.UpdateLetterDisplay(letter);
    }

    public void UpdateLetterDisplay(char letter) {
        gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().text = letter.ToString();
    }

    public void ResetAnswerSpot() {
        this.currentLetter = '-';
        isCorrect = false;
        gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,1);
        this.UpdateLetterDisplay(this.currentLetter);
    }

    public void SetTextColor(Color correctColor, Color incorrectColor, bool blackColor) {
        if(blackColor == true) gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
        else {
            if(isCorrect) gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().color = correctColor;
            else gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().color = incorrectColor;
        }
        //gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().outlineColor = new Color32(255,245,66,66);
    }

    public void HideElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 800f, this.transform.position.z);
    }

    public void ShowElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }
}
