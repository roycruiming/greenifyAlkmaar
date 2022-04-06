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

    //start van de achtergrond
    void Start()
    {
        InvokeRepeating("ChangeImage", 0f, WaitTime);
    }

    //verandert de achtergrondafbeelding naar een andere
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

    //kiest random afbeelding
    void ChooseRandomImage()
    {
        Number = Random.Range(0, Sprites.Count);
    }
}
