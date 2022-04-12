using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    private PowerUpHud PowerUpHud;
    //private PowerUp powerUp;

    private List<PowerUp> PowerUps;
    private int CurrentPowerUpIndex;

    public int MaxStamina;
    private int CurrentStamina;



   

    private void Awake()
    {
        PowerUps = new List<PowerUp>(); 
        PowerUps.Add(new Dash(2, gameObject));
        PowerUps.Add(new DoubleJump(2, gameObject));
        CurrentPowerUpIndex = 0; 
        


        if (MaxStamina == 0)
        {
            MaxStamina = 10; 
        }

        CurrentStamina = MaxStamina; 

        this.PowerUpHud = GameObject.Find("Powerbar").GetComponent<PowerUpHud>();
        
    }

    void Start()
    {
        PowerUpHud.SetMaxStamina(CurrentStamina);
        StartCoroutine(StartRefil());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentStamina - PowerUps[CurrentPowerUpIndex].cost >= 0)
        {
            if(PowerUps[CurrentPowerUpIndex].DoPowerUp() == true)
            {
                CurrentStamina = CurrentStamina - PowerUps[CurrentPowerUpIndex].cost;
                this.PowerUpHud.SetStamina(CurrentStamina);

            }       
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPowerUp();        
        }

    }

    private void SwitchPowerUp()
    {
        
        if (CurrentPowerUpIndex  == PowerUps.Count - 1)
        {
            CurrentPowerUpIndex = 0;
        }
        else {
            CurrentPowerUpIndex++;
        }

        //Sprite sprite = Resources.Load(PowerUps[CurrentPowerUpIndex].SpriteSource) as Sprite;
        Sprite s = Resources.Load<Sprite>(PowerUps[CurrentPowerUpIndex].SpriteSource); 
        
        PowerUpHud.ChangeIcon(s);    
        
        
    }



    private IEnumerator StartRefil()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(5f);       
        while (true)
        {
            if(CurrentStamina < MaxStamina) {           
                CurrentStamina = CurrentStamina + 1;
                this.PowerUpHud.SetStamina(CurrentStamina);
                
            }
            yield return waitForSeconds;
        }
    }

}