using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        //set puzzle difficulty based on difficulty number


        InitPuzzle();
    }

    private void InitPuzzle() {
        PickAndSetWord();
        SetAndDiplayInputLetters();
    }

    private void SetAndDiplayInputLetters() {
        List<char> allLetters = new List<char>();
        List<char> alphabet = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ").ToCharArray().ToList();
        allLetters.AddRange(this.currentWord.ToList());

        //add some extra letters for more difficulty
        for(int i = 0; i < (allLetters.Count + Random.Range(1,4)); i++) {
            allLetters.Add(alphabet[Random.Range(0,alphabet.Count)]);
        }

        foreach(char c in allLetters) print(c);
    }

    private List<string> GetAllPossibleTranslatedWords() {


        return null;
    }

    private void PickAndSetWord() {
        currentWord = this.allWords[Random.Range(0,allWords.Count)];
    }
}

public enum WordGuesserDifficulty { easy, normal, hard }
