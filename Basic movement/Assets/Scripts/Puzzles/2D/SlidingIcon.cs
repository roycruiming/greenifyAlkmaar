using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingIcon : MonoBehaviour
{
  int x;

    //Moves the icon by a set amount every fixed update
    void FixedUpdate()
    {
      transform.Translate(new Vector3(-5, 0, 0));
      if (x >= 90)
      {
        gameObject.SetActive(false);
      } else {
        x += 1;
      }
    }
}
