using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HowmanyDidYouSeePuzzle : MonoBehaviour
{
    public List<Text> ButtonText;
    public List<Sprite> DirtyIcons;
    public List<Sprite> CleanIcons;
    public GameObject PuzzlePanel;
    public GameObject ParentPanel;
    public Text Text;

    public ObjectivesController objCon;

    int Answer;
    int PuzzleDifficulty;
    int IconIndex;
    bool IsButtonPressed = false;
    bool MovingFinished = false;
    string ParentName;
    List<int> Values = new List<int>();


    //Start of the puzzle
    public void StartPuzzle(int difficulty, string Name)
    {
        PuzzlePanel.SetActive(true);
        ParentName = Name;
        Cursor.visible = true;

        Text.text =   GlobalGameHandler.GetTextByDictionaryKey("howmany did you see");

        PuzzlePanel.SetActive(true);

        PuzzleDifficulty = difficulty;

        CalculateAnswer();
        SetButtonValues();
        StartCoroutine(StartMovingIcons());
    }

    //Calculates a random answer and creates the icons
    void CalculateAnswer()
    {
      for(int i = 0; i < PuzzleDifficulty; i++)
      {
        switch (Random.Range(0,2))
        {
          case 0:
            SetIcons(DirtyIcons);
            break;
          case 1:
            SetIcons(CleanIcons);
            Answer += 1;
            break;
        }
      }
      Debug.Log("Answer: " + Answer);
    }

    //Set the buttons with the right values from low -> High
    void SetButtonValues()
    {
      Values.Add(Answer);
      for(int i = 0; i < 3; i++)
      {
        AddRandomValue();
      }
      Values.Sort();

      for(int i = 0; i < 4; i++)
      {
        ButtonText[i].text = Values[i].ToString();
      }
    }

    //Gives a random value
    void AddRandomValue()
    {
      int rand = Random.Range(1,PuzzleDifficulty +1);
      if (Values.Contains(rand)){
        AddRandomValue();
      } else {
        Values.Add(rand);
      }
    }


    //Creates the icons and sets them with the correct sprite
    void SetIcons(List<Sprite> Icons)
    {
      GameObject NewObj = new GameObject();
      NewObj.name = "Icon";
      NewObj.transform.localScale = new Vector3(1f , 1f, 1f);

      Image NewImage = NewObj.AddComponent<Image>();
      NewImage.sprite = Icons[Random.Range(0, Icons.Count)];
      NewImage.color = new Color32(255, 255, 255, 255);

      NewObj.AddComponent<SlidingIcon>();

      NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
      NewObj.transform.position = ParentPanel.transform.position;
      NewObj.transform.Translate(new Vector3(250, 0, 0));
      NewObj.SetActive(false);
    }

    //Let the icons move 1 for 1
    IEnumerator StartMovingIcons()
    {
      yield return new WaitForSeconds(3);
      StartCoroutine(MoveIcons());
    }

    //Moves the icons
    IEnumerator MoveIcons()
    {
      yield return new WaitForSeconds(1);
      if(IconIndex < ParentPanel.transform.childCount)
      {
        ParentPanel.transform.GetChild(IconIndex).gameObject.SetActive(true);
        IconIndex += 1;
        StartCoroutine(MoveIcons());
      } else {
        MovingFinished = true;
      }
    }

    //When an answer button has been pressed.
    public void ButtonPress(Button button)
    {
      if (!IsButtonPressed && MovingFinished)
      {
        IsButtonPressed = true;
        CheckAnswer(button.GetComponentInChildren<Text>().text);
      }
    }

    //Checks if the answer is correct
    void CheckAnswer(string GivenAnswer)
    {
      if(Answer.ToString() == GivenAnswer)
      {
        Correct();
      } else {
        StartCoroutine(Wrong());
      }
    }

    //When the answer is correct
    void Correct()
    {
      Text.text =  GlobalGameHandler.GetTextByDictionaryKey("correct answer");
      StartCoroutine(ClosePuzzle());
        GameObject.Find(ParentName).GetComponent<PuzzleController>().PuzzleCompleted(gameObject.name);

    }

    //When the answer is wrong
    IEnumerator Wrong()
    {
      Text.text =  GlobalGameHandler.GetTextByDictionaryKey("incorrect answer");
      StartCoroutine(ClosePuzzle());
      PuzzleController.PuzzlePlaying = false;
      yield return new WaitForSeconds(1);
      GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null, null, GlobalGameHandler.GetSentencesByDictionaryKey("ask for help"));
    }

    //Puzzle closes
    IEnumerator ClosePuzzle()
    {
      yield return new WaitForSeconds(1);

      Cursor.visible = false;
      IsButtonPressed = false;
      Answer = 0;
      PuzzleDifficulty = 0;
      IconIndex = 0;
      MovingFinished = false;
      Values.Clear();
      for(int i = 0; i < ParentPanel.transform.childCount; i++)
      {
         Destroy(ParentPanel.transform.GetChild(i).gameObject);
      }
      PuzzlePanel.SetActive(false);

    }
}
