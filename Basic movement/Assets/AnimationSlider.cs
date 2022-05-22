using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class AnimationSlider : MonoBehaviour
{


    private Animator anim;
    private Slider slider;
    public bool secondPlayer; 

    private GameObject player;
    private bool Pause = false; 




    // Use this for initialization
    void Start()
    {

        if (!secondPlayer) { 
        player = GameObject.Find("3RD Person");
        if (player == null) player = GameObject.Find("player");
        if(player == null) { Debug.Assert(false, "no player found"); }

        anim = player.GetComponent<Animator>();
        anim.applyRootMotion = false; 
        slider = this.GetComponent<Slider>();
        }

        else if (secondPlayer) {
            player = GameObject.Find("3RD Person");
            if (player == null) player = GameObject.Find("player2");
            if (player == null) { Debug.Assert(false, "no player found"); }

            anim = player.GetComponent<Animator>();
            anim.applyRootMotion = false;
            slider = this.GetComponent<Slider>();
        }
    }

    public void NextAnimation()
    {
        string currentAnimationName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        AnimatorClipInfo[] a = anim.GetCurrentAnimatorClipInfo(0);

        List<string> clips = anim.runtimeAnimatorController.animationClips.Select(clip => clip.name).ToList();
        int nextIndex = clips.IndexOf(currentAnimationName) + 1;

        if (clips.Count == nextIndex) nextIndex = 0;


        string nextAnimationName = clips[nextIndex];
        anim.Play(nextAnimationName);

    }





   

    public void OnsliderChanged() {

        string animationName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;


        anim.Play(animationName, -1, slider.normalizedValue);
        anim.speed = 1; 

    }

    public void onUseSliderToggled(bool isToggled) {

        print(isToggled);

        string animationName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (!isToggled)
        {
            anim.speed = 1; 
        }
        else {
            anim.speed = 0; 
        } 
    }






}


