using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonClickSound : MonoBehaviour
{
  public void playSound()
  {
    gameObject.GetComponent<AudioSource>().Play();
  }
}
