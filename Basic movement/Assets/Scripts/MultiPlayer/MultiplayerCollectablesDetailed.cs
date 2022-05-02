using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCollectablesDetailed : MonoBehaviour
{

    public GameObject quickCollectables;
    public GameObject detailedCollectables;


    // Start is called before the first frame update
    void Start()
    {
        quickCollectables = GameObject.Find("QuickCollectables");
        detailedCollectables = GameObject.Find("DetailedCollectables");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            detailedCollectables.SetActive(true);
            quickCollectables.SetActive(false);
        }
        else
        {
            detailedCollectables.SetActive(false);
            quickCollectables.SetActive(true);
        }
    }
}
