using UnityEngine;

public class DoubleJump : PowerUp
{


   


    public DoubleJump(int _cost, GameObject gameObject) : base(_cost, gameObject)
    {
        this.gameObject = gameObject;
        this.SpriteSource = "Sprites/PlaceHolder2";
       
    }

    public override bool DoPowerUp()
    {
        
        return  gameObject.GetComponent<SimpleSampleCharacterControl>().DoDoublJump();

       



        
    }

}