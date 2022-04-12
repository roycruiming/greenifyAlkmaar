using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PowerUp
{
    //private GameObject gameObject1;

    

    public Dash(int _cost, GameObject gameObject) : base(_cost, gameObject)
    {
      
        this.gameObject = gameObject; 
        this.SpriteSource = "Sprites/PlaceHolder";

    }

    public override bool DoPowerUp()
    {
        Rigidbody body =  gameObject.GetComponent<Rigidbody>();
        body.AddForce(gameObject.transform.forward * 1000);
        return true; 
    }


}
