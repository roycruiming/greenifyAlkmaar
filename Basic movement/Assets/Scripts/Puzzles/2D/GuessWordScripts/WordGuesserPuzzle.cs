using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WordGuesserPuzzle : MonoBehaviour
{
    int puzzleDifficulty;

    string currentWord = "";
    string forcedWord = null; //when pressing the quit button to skip a word and get a new one, should not be possible.
    char[] currentWordCorr;

    List<string> allWords = new List<string>();
    List<GuessWordAnswerSpot> currentAnswerSpots = new List<GuessWordAnswerSpot>();
    List<GuessWordsAnswerButton> currentAnswerButtons = new List<GuessWordsAnswerButton>();

    GameObject PuzzleControllerObject;

    private void checkCurrentInput() {
        bool everythingCorrect = true;
        for(int i = 0; i < currentWordCorr.GetLength(0); i++) 
            if(currentWordCorr[i] != currentAnswerSpots[i].currentLetter) {
                everythingCorrect = false;
                break;
            }
        
        if(everythingCorrect == false) StartCoroutine(showcaseTextAnimationAndHandleResult(new Color(1,0,0,1), everythingCorrect));
        else StartCoroutine(showcaseTextAnimationAndHandleResult(new Color(0,1,0,1), everythingCorrect)); //everything was filled in correct
        
    }

    IEnumerator showcaseTextAnimationAndHandleResult(Color textColor,bool everythingCorrect) {
        //showcase blink effect and after the effect reset the letters or reward the player
        float waitingTime = 0.35f;
        this.SetAnswerSpotsTextColors(textColor);
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(new Color(0,0,0,1)); //black
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(textColor);
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(new Color(0,0,0,1)); //black
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(textColor);
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(new Color(0,0,0,1)); //black
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(textColor);
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(new Color(0,0,0,1)); //black
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(textColor);
        yield return new WaitForSeconds(waitingTime);
        this.SetAnswerSpotsTextColors(new Color(0,0,0,1)); //black
        yield return new WaitForSeconds(waitingTime);

        if(everythingCorrect == false) ResetEverything();
        else PuzzleVictory();
    }

    public void ResetEverything() {
        foreach(GuessWordsAnswerButton gb in currentAnswerButtons) gb.ShowElement(); 
        foreach(GuessWordAnswerSpot gs in this.currentAnswerSpots) gs.ResetAnswerSpot();
    }

    private void ClosePuzzle() {
        Cursor.visible = false;
        this.gameObject.SetActive(false);
        
    }

    public void ForceClosePuzzle() {
        this.forcedWord = this.currentWord;
        this.ClosePuzzle();
        Cursor.lockState = CursorLockMode.Locked;
        PuzzleController.PuzzlePlaying = false;
    }

    private void PuzzleVictory() {
        this.ClosePuzzle();
        Cursor.visible = false;
        PuzzleControllerObject.GetComponent<PuzzleController>().PuzzleCompleted(gameObject.name);
    }

    private void SetAnswerSpotsTextColors(Color textColor) {
        foreach(GuessWordAnswerSpot gs in this.currentAnswerSpots) gs.SetTextColor(textColor);
    }

    private bool AllAnswersFilledIn() {
        bool everythingFilledIn = true;
        if(this.currentAnswerSpots.Count > 0) 
            foreach(GuessWordAnswerSpot gs in this.currentAnswerSpots) 
                if(gs.currentLetter == '-') {
                    everythingFilledIn = false;
                    break;
                }
            

        return everythingFilledIn;
    }

    public void StartPuzzle(WordGuesserDifficulty difficulty, string Name, GameObject puzzleParentController) {
        //load words from translation file.
        this.allWords = this.GetAllPossibleTranslatedWords(difficulty);

        this.gameObject.SetActive(true);
        this.transform.Find("Puzzle").gameObject.SetActive(true);

        this.PuzzleControllerObject = puzzleParentController;
        Cursor.visible = true;
        //set puzzle difficulty based on difficulty number


        InitPuzzle();
    }

    private void InitPuzzle() {
        PickAndSetWord();
        SetAndDiplayInputLetters();
        SetAnswerDisplay();
    }

    public bool SetLetter(char letter) {
        GuessWordAnswerSpot gs = GetFirstUnsetAnswerSpot();
        if(gs != null) {
            bool correctLetterInSpot = false;
            if(currentWordCorr[gs.rowIndex] == letter) correctLetterInSpot = true;
            gs.setAndUpdateLetterDisplay(letter, correctLetterInSpot);
            if(this.AllAnswersFilledIn()) this.checkCurrentInput();
            return true;
        }
        else {
            if(this.AllAnswersFilledIn()) this.checkCurrentInput();
            return false;
        }
    }

    private GuessWordAnswerSpot GetFirstUnsetAnswerSpot() {
        foreach(GuessWordAnswerSpot gs in currentAnswerSpots) if(gs.currentLetter == '-') return gs;

        return null;
    }

    private void SetAnswerDisplay() {
        //clear previous answer spots
        this.currentAnswerSpots.Clear();

        for(int i = 0; i < 12; i++)  
            if(GameObject.Find("letter_answer_display_" + i) != null) {
                if(i < currentWord.Length) {
                    GuessWordAnswerSpot gs = GameObject.Find("letter_answer_display_" + i).GetComponent<GuessWordAnswerSpot>();
                    gs.currentLetter = '-';
                    gs.UpdateLetterDisplay('-');
                    gs.ShowElement();
                    currentAnswerSpots.Add(gs);
                }
                else GameObject.Find("letter_answer_display_" + i).GetComponent<GuessWordAnswerSpot>().HideElement();
            }
    }

    private void SetAndDiplayInputLetters() {
        List<char> allLetters = currentWord.ToCharArray().ToList();
        List<char> alphabet = ("AAABCDEEEFGHIIJKLMNNOOPQRSTUUVWXYZ").ToCharArray().ToList();
        
        for(int i = 0; i < UnityEngine.Random.Range(2,5); i++) {
            allLetters.Add(alphabet[UnityEngine.Random.Range(0,alphabet.Count)]);
        }

        this.RandomlySetLetters(allLetters);

        //foreach(char c in allLetters) print(c);
    }

    private void RandomlySetLetters(List<char> allLetters) {
        allLetters = RandommizedItemsList(allLetters);
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("LetterAnswerButt").Count(); i++) {
            GameObject answerButton = null;
            foreach(GameObject anwserButt in  GameObject.FindGameObjectsWithTag("LetterAnswerButt")) {
                if(anwserButt.GetComponent<GuessWordsAnswerButton>().rowIndex == i) {
                    answerButton = anwserButt;
                    break;
                }
            }

            if(answerButton != null) {
                if(i < allLetters.Count) {
                    answerButton.GetComponent<GuessWordsAnswerButton>().ShowElement();
                    answerButton.GetComponent<GuessWordsAnswerButton>().SetAndDisplayLetter(allLetters[i]);
                    currentAnswerButtons.Add(answerButton.GetComponent<GuessWordsAnswerButton>());
                }
                else answerButton.GetComponent<GuessWordsAnswerButton>().HideElement();
            }
        }
    }

    private List<char> RandommizedItemsList(List<char> originalList) {
        List<char> randomizedList = new List<char>();
        do {
             int randomIndex = UnityEngine.Random.Range(0,originalList.Count);
             randomizedList.Add(originalList[randomIndex]);
             originalList.RemoveAt(randomIndex);


        } while (originalList.Count > 0);

        return randomizedList;
    }

    private List<string> GetAllPossibleTranslatedWords(WordGuesserDifficulty difficulty) {
        //length 12 is the max amount of characters that can be used,
        List<string> allWords = new List<string>();
        int maxWordLength = 100;
        if(difficulty == WordGuesserDifficulty.easy) maxWordLength = 8;
        else if(difficulty == WordGuesserDifficulty.normal) maxWordLength = 10;
        else maxWordLength = 12;

        for(int i = 0; i < 200; i++) {
            string translatedWord = GlobalGameHandler.GetTextByDictionaryKey("guess word " + i);
            if(translatedWord != "ERROR WORD NOT FOUND") {
                if(translatedWord.Length <= maxWordLength) allWords.Add(translatedWord);
            }
            else break;
        }

        return allWords;
    }

    private void PickAndSetWord() {
        if(forcedWord == null) {
            //DEPENDING ON THE DIFFICULTY CHOOSE A WORD THAT HAS A MATCHING AMOUNT OF CHARACTERS BASED ON THE DIFFICULTY
            currentWord = this.allWords[UnityEngine.Random.Range(0,allWords.Count)].ToUpper();
        }
        else {
            currentWord = forcedWord;
            forcedWord = null;
        }

        currentWordCorr = currentWord.ToArray();
        print(currentWord);
    }
}

public enum WordGuesserDifficulty { easy, normal, hard }
