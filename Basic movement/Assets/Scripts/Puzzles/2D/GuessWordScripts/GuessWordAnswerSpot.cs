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
        print("inside set and display");
        this.currentLetter = letter;
        this.isCorrect = isPlacedCorrect;
        this.UpdateLetterDisplay(letter);
    }

    public void UpdateLetterDisplay(char letter) {
        gameObject.transform.Find("letter").GetComponent<TextMeshProUGUI>().text = letter.ToString();
    }

    public void resetAnswerSpot() {
        this.currentLetter = '-';
        isCorrect = false;
    }

    public void HideElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 800f, this.transform.position.z);
    }

    public void ShowElement() {
        this.transform.position = new Vector3(this.transform.position.x, this.initialY, this.transform.position.z);
    }
}
