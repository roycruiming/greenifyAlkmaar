using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp 
{

    public int cost;
    public GameObject gameObject;
    public string SpriteSource;  
    

    public PowerUp(int _cost, GameObject gameObject) {
        this.cost = _cost;
        this.gameObject = gameObject;
    }

    public abstract bool DoPowerUp(); 


    // Start is called before the first frame update

}
