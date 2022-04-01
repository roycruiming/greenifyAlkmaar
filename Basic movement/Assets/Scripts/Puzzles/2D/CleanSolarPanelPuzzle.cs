using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanSolarPanelPuzzle : MonoBehaviour
{

    public GameObject PuzzlePanel;
    public List<Sprite> SpriteSources;
    public GameObject ParentPanel;
    public Text PercentText;

    public int SpreadAmount = 400;
    int PercentCompleted;
    int PercentAmount;
    int AmountCompleted;
    int TotalTrash;
    Color32 PercentColor;
    Sprite LastSprite;
    public static bool IsPlaying = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           StartPuzzle(5);
        }
    }

    void StartPuzzle(int difficulty)
    {
        IsPlaying = true;
        Cursor.visible = true;

        PercentCompleted = Random.Range(3, 24);
        ChangeText(PercentCompleted);

        PercentAmount = (100 - PercentCompleted) / difficulty;
        TotalTrash = difficulty;

        CreateImages(difficulty);
        PuzzlePanel.SetActive(true);
    }

    void CreateImages(int amount)
    {
      for(int i = 0; i < amount; i++)
      {
        GameObject NewObj = new GameObject();
        NewObj.name = "Trash";

        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.sprite = SpriteSources[Random.Range(0, SpriteSources.Count)];

        NewObj.AddComponent<BoxCollider2D>();
        NewObj.AddComponent<DragAndDrop>();

        NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
        NewObj.transform.position = ParentPanel.transform.position;
        NewObj.transform.Translate(new Vector3(Random.Range(-SpreadAmount, SpreadAmount), Random.Range(-SpreadAmount, SpreadAmount), 0));
        NewObj.SetActive(true);
      }
    }

    void ChangeText(int number)
    {
      if(PercentText) {
        if(number < 25) {
          PercentColor = new Color32(204, 51, 51, 255);
        } else if (number >= 25 && number < 60) {
          PercentColor = new Color32(204, 93, 51, 255);
        } else if (number >= 60 && number < 80) {
          PercentColor = new Color32(204, 202, 51, 255);
        } else {
          PercentColor = new Color32(73, 204, 51, 255);
        }

          PercentText.color = PercentColor;
          PercentText.text = number.ToString() + "%";
      }

    }


    public void UpdateProgress(){
      PercentCompleted += PercentAmount;
      ChangeText(PercentCompleted);

      AmountCompleted += 1;
      CheckCompletion();
    }

    void CheckCompletion()
    {
      if (AmountCompleted == TotalTrash)
      {
        PercentCompleted = 100;
        ChangeText(PercentCompleted);

        StartCoroutine(StopPuzzle());
      }
    }

    IEnumerator StopPuzzle(){
        yield return new WaitForSeconds(5);

        IsPlaying = false;
        Cursor.visible = false;
        
        Destroy(gameObject);
   }



}
