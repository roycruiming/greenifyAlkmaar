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

    //Start of the background
    void Start()
    {
        InvokeRepeating("ChangeImage", 0f, WaitTime);
    }

    //Changes the background image to a different one
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

    //Chooses a random image from the list
    void ChooseRandomImage()
    {
        Number = Random.Range(0, Sprites.Count);
    }
}
