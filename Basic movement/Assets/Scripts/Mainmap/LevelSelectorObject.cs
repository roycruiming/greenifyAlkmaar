using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorObject : MonoBehaviour
{

    public string levelNameDutch;
    public string levelNameEnglish;
    public int index;
    public int isActive = 0;

    public Object levelScene;

    public bool isUnlocked;
    float posX = 0;
    float poxY = 0;
    float posZ = 0;

    public void Awake() {
        //posX = transform.position.x;
        // Assigns a material named "Assets/Resources/DEV_Orange" to the object.
        if(this.isUnlocked == false) {
            Material disabledMaterial = Resources.Load("DisabledLevel", typeof(Material)) as Material;
            GetComponent<Renderer>().material = disabledMaterial;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
