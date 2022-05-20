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
        slider = this.GetComponent<Slider>(); 
    }



   

    public void OnsliderChanged() {

        
        anim.speed = 0;



        if (anim != null)
        {
            anim.Play("Idle", -1, slider.normalizedValue); 
        }
    }

    public void OnDragEnded() { 

        //anim.Play()
        //anim.GetCurrentAnimatorStateInfo().
    
    }




}


