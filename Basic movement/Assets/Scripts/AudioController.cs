using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource walkSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && SimpleSampleCharacterControl.m_isGrounded == true)
        {
            walkSound.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            walkSound.Stop();
        }
        if(SimpleSampleCharacterControl.m_isGrounded == false)
        {
            walkSound.Stop();
        }

    }
}
