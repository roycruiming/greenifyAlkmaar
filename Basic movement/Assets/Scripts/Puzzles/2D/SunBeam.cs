using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBeam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject home;
    public GameObject target;
    public Sprite beamSprite;
    
    GameObject beamObject;
    void Start()
    {
        beamObject = new GameObject();
        beamObject.name = "Beam";

        Image beamImage = beamObject.AddComponent<Image>();
        beamImage.sprite = beamSprite;

        beamObject.transform.SetParent(home.transform);
        beamObject.transform.position = new Vector3(0, 0, 0);

        beamObject.SetActive(true);

        

    }

        // Update is called once per frame
    void Update()
    {
        home.transform.localScale += new Vector3(0, 1, 0);



    }
}
