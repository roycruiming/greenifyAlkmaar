using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanSolarPanelPuzzle : MonoBehaviour
{

    public GameObject PuzzlePanel;
    public List<Image> Sprites;
    public List<Sprite> SpriteSources;
    Sprite LastSprite;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           StartPuzzle();
        }
    }

    void StartPuzzle()
    {
        SetSpriteImages();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PuzzlePanel.SetActive(true);

        //TIJDELIJK
        Time.timeScale = 0f;
    }

    void SetSpriteImages()
    {
        foreach(Image n in Sprites)
        {
            n.sprite = SpriteSources[Random.Range(0, SpriteSources.Count)];
        }
    }
}
