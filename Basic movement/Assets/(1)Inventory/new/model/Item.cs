using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Item : MonoBehaviour
{
    public List<GameObject> unlockAbles;

    public float RotationOffsetY = 0;
    public float HeightOffsetY;
    public bool OverwriteHeightOffsetByInitialItemPosition = true;
    public Sprite HudImage;


    private void Update()
    {
        
    }

    void Awake()      
    {
        if (OverwriteHeightOffsetByInitialItemPosition) { HeightOffsetY = gameObject.transform.position.y; } 
    }

}