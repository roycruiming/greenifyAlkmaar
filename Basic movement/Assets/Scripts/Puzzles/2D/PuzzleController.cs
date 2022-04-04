using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
  public GameObject PuzzleCanvas;
  public List<GameObject> Puzzles;
  public int PuzzleDifficulty = 3;
  int SelectedPuzzle;
  string CorrectScript;

  void Update()
  {
      //Als de speler op enter drukt en nu nog geen puzzel speelt
      if (Input.GetKeyDown(KeyCode.Return) && !CleanSolarPanelPuzzle.IsPlaying && !HowmanyDidYouSeePuzzle.IsPlaying)
      {
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
            Puzzles[SelectedPuzzle].GetComponent<CleanSolarPanelPuzzle>().StartPuzzle(PuzzleDifficulty);
            break;
          case "HowmanyPuzzle":
            Puzzles[SelectedPuzzle].GetComponent<HowmanyDidYouSeePuzzle>().StartPuzzle(PuzzleDifficulty);
            break;
        }

      }
  }
}
