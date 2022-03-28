using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingIconController : MonoBehaviour
{

    public GameObject recycleSprite;
    public GameObject gasSprite;
    public GameObject factorySprite;
    public GameObject windmillSprite;
    public GameObject solarSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator loadSequence(GameObject[] listToShow)
    {
        //wait before starting, then slowly spawn each icon one by one
        yield return new WaitForSeconds(2f);
        createImage(listToShow[0]);
        yield return new WaitForSeconds(1f);
        createImage(listToShow[1]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[2]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[3]);
        yield return new WaitForSeconds(0.7f);
        createImage(listToShow[4]);
    }

    public GameObject[] createSpriteList()
    {
        // create an array for all possible sprites
        GameObject[] spriteList = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            // fill the list with random sprites
            switch (Random.Range(1, 6))
            {
                case 1:
                    spriteList[i] = windmillSprite;
                    break;
                case 2:
                    spriteList[i] = solarSprite;
                    break;
                case 3:
                    spriteList[i] = gasSprite;
                    break;
                case 4:
                    spriteList[i] = recycleSprite;
                    break;
                case 5:
                    spriteList[i] = factorySprite;
                    break;
            }
        }

        return spriteList;
    }

    public void createImage(GameObject spriteToSpawn)
    {
        //instantiate the sprite given at the start position with no rotation
        GameObject testImage = Instantiate(spriteToSpawn, new Vector3(105, 0, 0), Quaternion.identity) as GameObject;

        //make sure the position is relative to the parent!
        testImage.transform.SetParent(transform, false);
        //This causes the images to appear in front of everything but still be hidden by two white squares, creating the rotating in/out effect
        testImage.transform.SetSiblingIndex(4);
    }
}
