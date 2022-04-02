using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
  public List<GameObject> Puzzles;
  public int PuzzleDifficulty = 3;
  int SelectedPuzzle;
  string CorrectScript;

  void Update()
  {
      if (Input.GetKeyDown(KeyCode.Return) && !CleanSolarPanelPuzzle.IsPlaying && !HowmanyDidYouSeePuzzle.IsPlaying)
      {
        SelectedPuzzle = Random.Range(0, Puzzles.Count);
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
