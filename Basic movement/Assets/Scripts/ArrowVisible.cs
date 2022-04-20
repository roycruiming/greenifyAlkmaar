using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVisible : MonoBehaviour
{
    public GameObject arrow;
    public bool arrowVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.Find("DirectionalArrow");
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowVisible)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}
