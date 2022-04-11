using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp 
{

    public int cost;

    public PowerUp(int _cost) {
        this.cost = _cost;     
    }

    public abstract void DoPowerUp(); 


    // Start is called before the first frame update

}
