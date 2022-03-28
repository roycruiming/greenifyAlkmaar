using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingIconModel : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 0;
    public EnergyType energy;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        GetComponent<RectTransform>().anchoredPosition += new Vector2(-2f, 0);

        if (timer > 100)
        {
            Destroy(gameObject);
        }


    }

}
