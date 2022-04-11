using UnityEngine;
using UnityEngine.UI;

public class PowerUpHud : MonoBehaviour
{

    private Slider slider;

    private void Awake()
    {
        slider = this.GetComponent<Slider>(); 
    }

    // Start is called before the first frame update


    public void SetStamina(int stamina) {
        slider.value = stamina; 
    }

    public void SetMaxStamina(int stamina) {

        slider.maxValue = stamina;
        slider.value = stamina; 
    }
}
