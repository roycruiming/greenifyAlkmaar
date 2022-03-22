﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicImageController : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 0;
    public EnergyType energy;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        GetComponent<RectTransform>().anchoredPosition += new Vector2(-0.6f, 0);

        if (timer > 350)
        {
            Destroy(gameObject);
        }


    }

}