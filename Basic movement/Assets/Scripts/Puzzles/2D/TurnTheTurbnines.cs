using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTheTurbnines : MonoBehaviour
{
    public Text EnergyText;
    public Image LeftTurbine;
    public Image MiddleTurbine;
    public Image RightTurbine;
    public Image WindDirArrow;

    public GameObject Puzzle;

    private string WindDir = "right";
    private string TurbineLeftDir = "left";
    private string TurbineMiddleDir = "left";
    private string TurbineRightDir = "left";


    double Power = 0;
    int PuzzleDifficulty;
    bool IsPuzzleDone = false;
    bool WindFlippable = true;
    Color32 PercentColor;
    string ParentName;


    public void StartPuzzle(int Difficulty, string Name)
    {
      Puzzle.SetActive(true);
      Cursor.visible = true;
      PuzzleDifficulty = Difficulty;
      ParentName = Name;
    }

    void FixedUpdate(){
      if(!IsPuzzleDone && PuzzleController.PuzzlePlaying){
        CheckDirection(TurbineLeftDir);
        CheckDirection(TurbineMiddleDir);
        CheckDirection(TurbineRightDir);

        UpdateEnergy();

        CheckIfWindFlip();
      }
    }

    void CheckDirection(string Turbine)
    {
      if (WindDir == Turbine) {
        if (Power >= 100){
          PuzzleVictory();
        } else {
          Power += 0.01;
        }
      } else {
        if (Power > 0) {
          Power -= (0.002 * PuzzleDifficulty);
        }
      }
    }

    public void ButtonPress(string Dir)
    {
      switch (Dir)
      {
        case "leftleft":
          TurbineLeftDir = "left";
          break;
        case "leftright":
          TurbineLeftDir = "right";
          break;
        case "middleleft":
          TurbineMiddleDir = "left";
          break;
        case "middleright":
          TurbineMiddleDir = "right";
          break;
        case "rightleft":
          TurbineRightDir = "left";
          break;
        case "rightright":
          TurbineRightDir = "right";
          break;
      }

      TurnTurbines();
    }

    private void TurnTurbines()
    {
      if (TurbineLeftDir == "left"){
        LeftTurbine.GetComponent<Image>().transform.localScale = new Vector3(1,1,1);
      } else {
        LeftTurbine.GetComponent<Image>().transform.localScale = new Vector3(-1,1,1);
      }

      if (TurbineMiddleDir == "left"){
        MiddleTurbine.GetComponent<Image>().transform.localScale = new Vector3(1,1,1);
      } else {
        MiddleTurbine.GetComponent<Image>().transform.localScale = new Vector3(-1,1,1);
      }

      if (TurbineRightDir == "left"){
        RightTurbine.GetComponent<Image>().transform.localScale = new Vector3(1,1,1);
      } else {
        RightTurbine.GetComponent<Image>().transform.localScale = new Vector3(-1,1,1);
      }
    }

    private void CheckIfWindFlip()
    {
      if (WindFlippable){
        print("YO");
        StartCoroutine(ChangeWind(Random.Range(5,8)));
        WindFlippable = false;
      }
    }

    IEnumerator ChangeWind(int WaitTime)
    {
      if(WindDir == "left"){
        WindDir = "right";
        WindDirArrow.GetComponent<Image>().transform.localScale = new Vector3(-1,1,1);
      } else {
        WindDir = "left";
        WindDirArrow.GetComponent<Image>().transform.localScale = new Vector3(1,1,1);
      }
      yield return new WaitForSeconds(WaitTime);
      WindFlippable = true;
    }

    private void UpdateEnergy()
    {
      EnergyText.text = Mathf.RoundToInt((float)Power).ToString() + "%";
      if(EnergyText) {
        if(Power < 25) {
          PercentColor = new Color32(204, 51, 51, 255);
        } else if (Power >= 25 && Power < 60) {
          PercentColor = new Color32(204, 93, 51, 255);
        } else if (Power >= 60 && Power < 80) {
          PercentColor = new Color32(204, 202, 51, 255);
        } else {
          PercentColor = new Color32(73, 204, 51, 255);
        }

        EnergyText.color = PercentColor;
    }
  }


    private void PuzzleVictory(){
      IsPuzzleDone = true;
      GameObject.Find(ParentName).GetComponent<PuzzleController>().PuzzleCompleted();
      StartCoroutine(ClosePuzzle());
    }

    IEnumerator ClosePuzzle()
    {
      yield return new WaitForSeconds(1);

      IsPuzzleDone = false;
      Cursor.visible = false;
      Power = 0;
      PuzzleDifficulty = 0;
      WindFlippable = true;

      gameObject.transform.parent.gameObject.SetActive(false);
    }
}
