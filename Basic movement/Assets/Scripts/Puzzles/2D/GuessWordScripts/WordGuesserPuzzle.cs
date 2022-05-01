using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WordGuesserPuzzle : MonoBehaviour
{
    int puzzleDifficulty;

    string currentWord = "";

    List<string> allWords = new List<string>();

    public void StartPuzzle(WordGuesserDifficulty difficulty, string Name) {
        //TEST WORDS:
        allWords.Add("zonnestraal");
        allWords.Add("windmolen");
        allWords.Add("alkmaar");
        //END TEST WORDS

        this.gameObject.SetActive(true);
        this.transform.Find("Puzzle").gameObject.SetActive(true);

        //set puzzle difficulty based on difficulty number


        InitPuzzle();
    }

    private void InitPuzzle() {
        PickAndSetWord();
        SetAndDiplayInputLetters();
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
        print(allLetters.Count);
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
                }
                else answerButton.GetComponent<GuessWordsAnswerButton>().HideElement();
            }
        }

        // int counter = -1;
        // int initialLettersCount = allLetters.Count;
        // do {
        //     counter++;
        //     int randomIndex = Random.Range(0,allLetters.Count);
        //     print("Set letter " + allLetters[randomIndex] + " in letter_answer_container_" + counter);
        //     GameObject.Find("letter_answer_container_" + counter).GetComponent<GuessWordsAnswerButton>().SetAndDisplayLetter(allLetters[randomIndex]);
        //     allLetters.RemoveAt(randomIndex);

        // } while (allLetters.Count > 0);
        // print(GameObject.FindGameObjectsWithTag("LetterAnswerButt").Count());
        // print(initialLettersCount);
        
        //max letters is 16; so hide all the other answer buttons that are not used
        // for(int j = initialLettersCount; j < GameObject.FindGameObjectsWithTag("LetterAnswerButt").Count(); j++) {
        //     print("Hide" + "letter_answer_container_" + j); 
        //     if(GameObject.Find("letter_answer_container_" + j) != null) GameObject.Find("letter_answer_container_" + j).GetComponent<GuessWordsAnswerButton>().HideElement();
        // }
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

    private List<string> GetAllPossibleTranslatedWords() {


        return null;
    }

    private void PickAndSetWord() {
        //!! NEED TO SET RESTRICTION OF MAXIMUM CHARACTERS FOR TRANSLATED WORDS !!
        currentWord = this.allWords[UnityEngine.Random.Range(0,allWords.Count)].ToUpper();
        print(currentWord);
    }
}

public enum WordGuesserDifficulty { easy, normal, hard }
