using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPuzzle : MonoBehaviour
{

    int AmountSolved;
    int PuzzleDifficulty;
    string ParentName;
    bool WhichPair = false;
    int CountChecked = 0;
    public static bool CanClickOthers = true;

    Sprite FirstChoice;
    GameObject FirstCard;
    Sprite SecondChoice;
    GameObject SecondCard;

    public List<Sprite> Pairs;

    public Sprite CardBack;
    public GameObject PuzzlePanel;
    public List<Sprite> SpriteSources;
    public GameObject ParentPanel;
    // Start is called before the first frame update

    public void StartPuzzle(int Difficulty, string Name){
      PuzzlePanel.SetActive(true);
      PuzzleDifficulty = 8;
      ParentName = Name;
      Cursor.visible = true;
      setIcons();
      MakeCards();
    }

    public void setIcons(){
      for(int x = 0; x < 2; x++){
        for(int i = 0; i < SpriteSources.Count; i++){
          Pairs.Add(SpriteSources[i]);
        }
      }

      for (int i = 0; i < Pairs.Count; i++) {
         Sprite temp = Pairs[i];
         int randomIndex = Random.Range(i, Pairs.Count);
         Pairs[i] = Pairs[randomIndex];
         Pairs[randomIndex] = temp;
     }
    }

    public void MakeCards(){
      for (int i = 0; i < PuzzleDifficulty * 2; i++){
        GameObject NewObj = new GameObject();
        NewObj.name = "Card";
        Image NewImg = NewObj.AddComponent<Image>();
        NewImg.sprite = CardBack;
        NewObj.AddComponent<MemoryCard>();
        NewObj.GetComponent<MemoryCard>().IconSprite = Pairs[i];
        NewObj.GetComponent<MemoryCard>().CanBeClicked = true;
        NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
      }
    }

    public void ShowCard(GameObject Card, Sprite Icon){
      if(CountChecked < 1 ){
        FirstChoice = Icon;
        FirstCard = Card;
        Card.GetComponent<Image>().overrideSprite = Icon;
        CountChecked++;
      } else if(CountChecked < 2) {
        SecondChoice = Icon;
        SecondCard = Card;
        Card.GetComponent<Image>().overrideSprite = Icon;
        CountChecked++;
        CanClickOthers = false;
        CheckPair();

      }
    }

    public void CheckPair(){
      if(FirstChoice == SecondChoice){
        StartCoroutine(CheckForCompletion());
      } else {
        StartCoroutine(ClosePair());
      }
    }

    IEnumerator ClosePair(){
      yield return new WaitForSeconds(2);
      FirstCard.GetComponent<Image>().overrideSprite = null;
      SecondCard.GetComponent<Image>().overrideSprite = null;
      CanClickOthers = true;
      CountChecked = 0;
      FirstCard.GetComponent<MemoryCard>().CanBeClicked = true;
      SecondCard.GetComponent<MemoryCard>().CanBeClicked = true;
    }

    IEnumerator CheckForCompletion(){
      yield return new WaitForSeconds(1);
      AmountSolved++;
      CountChecked = 0;
      CanClickOthers = true;
      FirstCard.GetComponent<Image>().color = new Color32(204, 51, 51, 0);
      SecondCard.GetComponent<Image>().color = new Color32(204, 51, 51, 0);
      FirstCard.GetComponent<MemoryCard>().CanBeClicked = false;
      SecondCard.GetComponent<MemoryCard>().CanBeClicked = false;
      if(AmountSolved >= 8){
        StartCoroutine(PuzzleCompleted());
      }
    }

    IEnumerator PuzzleCompleted(){
      FirstCard = null;
      SecondCard = null;
      CountChecked = 0;
      PuzzleDifficulty = 0;
      FirstChoice = null;
      SecondChoice = null;
      Pairs.Clear();
      AmountSolved = 0;

      foreach(Transform child in ParentPanel.transform)
      {
        GameObject.Destroy(child.gameObject);
      }

      yield return new WaitForSeconds(1);
      Cursor.visible = false;
      PuzzlePanel.SetActive(false);
      GameObject.Find(ParentName).GetComponent<PuzzleController>().PuzzleCompleted(gameObject.name);
    }
}
