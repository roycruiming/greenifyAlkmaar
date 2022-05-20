using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSlider : MonoBehaviour
{


    private Animator anim;
    private Slider slider; 
    

     

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.Find("3RD Person");
        if (player == null) player = GameObject.Find("player");
        if(player == null) { Debug.Assert(false, "no player found"); }
        anim = player.GetComponent<Animator>();
        anim.speed = 0;

        slider = this.GetComponent<Slider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null) {
            anim.Play("Idle", -1, slider.normalizedValue);
            //anim.Play("f", -1, )
        }

        
    }


}


