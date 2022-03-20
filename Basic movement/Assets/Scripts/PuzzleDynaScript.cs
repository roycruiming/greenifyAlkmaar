using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDynaScript : MonoBehaviour
{
    public GameObject recycleSprite;
    public GameObject gasSprite;
    public GameObject factorySprite;
    public GameObject windmillSprite;
    public GameObject solarSprite;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator loadSequence()
    {
        GameObject[] listToShow = createSpriteList();
        
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

    GameObject[] createSpriteList()
    {
        GameObject[] spriteList = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            switch(Random.Range(1, 5))
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

    void createImage(GameObject spriteToSpawn)
    {
        GameObject testImage = Instantiate(spriteToSpawn, new Vector3(105, 0, 0), Quaternion.identity) as GameObject;
        
        testImage.transform.SetParent(transform, false);
        testImage.transform.SetSiblingIndex(4);
    }
}
