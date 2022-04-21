using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MemoryCard : MonoBehaviour, IPointerClickHandler
{
  public Sprite IconSprite;
  public bool CanBeClicked;
  MemoryPuzzle PuzzleScript;

  public void Start(){
    PuzzleScript = GameObject.FindGameObjectWithTag("MemoryPuzzle").GetComponent<MemoryPuzzle>();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if(CanBeClicked){
      PuzzleScript.ShowCard(gameObject, IconSprite);
      CanBeClicked = false;
    }
  }
}
