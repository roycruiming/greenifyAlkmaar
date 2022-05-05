using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{

    private Animator Animator;
    float previousDistance;
    private bool hasInterActed = false; 



    private void Awake()
    {
        Animator =  this.GetComponent<Animator>();
        
    }

    public void SetInterActionComplete() {
        Animator.SetBool("isWaving", false);
        hasInterActed = true;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player =  GameObject.Find("3RD Person");
        if (player == null) return;
        
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < 15)
            transform.LookAt(player.transform);
        if (hasInterActed == true) 
            return; 
        if (distance < 15 && !Animator.GetBool("isWaving") && !hasInterActed) 
            Animator.SetBool("isWaving", true);
        else if (Animator.GetBool("isWaving") && distance >= 15) {
            Animator.SetBool("isWaving", false);
            hasInterActed = true; 
        }
    }
}


