using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    public Image BackgroundImage;

    public List<Sprite> Sprites;
    public int WaitTime = 10;
    int Number = 1;
    Sprite CurrentImage;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeImage", 0f, WaitTime);
    }

    void ChangeImage()
    {
        ChooseRandomImage();

        if(CurrentImage == Sprites[Number])
        {
            ChangeImage();
        }

        CurrentImage = Sprites[Number];
        BackgroundImage.sprite = Sprites[Number];

    }

    void ChooseRandomImage()
    {
        Number = Random.Range(0, Sprites.Count);
    }
}
