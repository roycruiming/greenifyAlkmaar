using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PowerUp
{
    private GameObject gameObject1;

    public Dash(int _cost, GameObject gameObject) : base(_cost)
    {
        gameObject1 = gameObject;

    }

    public override void DoPowerUp()
    {
        Rigidbody body =  gameObject1.GetComponent<Rigidbody>();
        body.AddForce(gameObject1.transform.forward * 1000); 
    }


}
