using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
  GameObject PuzzleCanvas;
  ObjectivesController objectivesController;
  public List<GameObject> Puzzles;
  public int PuzzleDifficulty = 3;
  public int MoneyRange = 10;
  int SelectedPuzzle;
  public static bool PuzzlePlaying = false;

void Start()
{
  PuzzleCanvas = GameObject.Find("PuzzleCanvas");
}


  public void StartAPuzzle()
  {
      //Als de speler op enter drukt en nu nog geen puzzel speelt
        SelectedPuzzle = Random.Range(0, Puzzles.Count);

        //set elke puzzel op inactief
        for(int i = 0; i < PuzzleCanvas.transform.childCount; i++)
        {
           PuzzleCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        //set alleen de juiste puzzle op actief
        Puzzles[SelectedPuzzle].gameObject.SetActive(true);

        //welke puzzel speel je en welk script hoort daarbij
        switch (Puzzles[SelectedPuzzle].name.ToString())
        {
          case "CleanPuzzle":
            Puzzles[SelectedPuzzle].GetComponent<CleanSolarPanelPuzzle>().StartPuzzle(PuzzleDifficulty, transform.name);
            break;
          case "HowmanyPuzzle":
            Puzzles[SelectedPuzzle].GetComponent<HowmanyDidYouSeePuzzle>().StartPuzzle(PuzzleDifficulty, transform.name);
            break;
          case "MemoryPuzzle":
            Puzzles[SelectedPuzzle].GetComponent<MemoryPuzzle>().StartPuzzle(PuzzleDifficulty, transform.name);
            break;
          case "TurnTheTurbine":
            Puzzles[SelectedPuzzle].GetComponent<TurnTheTurbnines>().StartPuzzle(PuzzleDifficulty, transform.name);
            break;
        }

        PuzzlePlaying = true;
  }


  public void PuzzleCompleted()
  {
    transform.Find("Smoke").gameObject.SetActive(false);
    gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    if(objectivesController){
          objectivesController.DeleteItemInList(this);
    }
    PuzzlePlaying = false;
    GlobalGameHandler.GivePlayerCoints(Random.Range(MoneyRange / 2, MoneyRange * 2 ));
  }
}
