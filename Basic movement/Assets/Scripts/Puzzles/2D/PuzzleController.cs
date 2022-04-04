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
      if (Input.GetKeyDown(KeyCode.Return) && !CleanSolarPanelPuzzle.IsPlaying && !HowmanyDidYouSeePuzzle.IsPlaying)
      {
        SelectedPuzzle = Random.Range(0, Puzzles.Count);

        for(int i = 0; i < PuzzleCanvas.transform.childCount; i++)
        {
           PuzzleCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        Puzzles[SelectedPuzzle].gameObject.SetActive(true);

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
