using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingIcon : MonoBehaviour
{
  int x;

    void Update()
    {
      transform.Translate(new Vector3(-1, 0, 0));
      if (x >= 500)
      {
        gameObject.SetActive(false);
      } else {
        x += 1;
      }
    }
}
