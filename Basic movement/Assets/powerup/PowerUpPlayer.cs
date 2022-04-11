using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    private PowerUpHud PowerUpHud;
    private PowerUp powerUp; 

    public int MaxStamina;
    private int CurrentStamina;

    private void Awake()
    {
        powerUp = new Dash(2, gameObject); 


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
        if (Input.GetKeyDown(KeyCode.E) && CurrentStamina - powerUp.cost >= 0)
        {
            CurrentStamina = CurrentStamina - powerUp.cost;
            this.PowerUpHud.SetStamina(CurrentStamina);
            powerUp.DoPowerUp(); 
        }
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