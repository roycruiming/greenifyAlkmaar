using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkerScript : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StopAnimation()
    {
        animator.speed = 0;
    }


}
