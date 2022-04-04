using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingIcon : MonoBehaviour
{
  int x;

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
