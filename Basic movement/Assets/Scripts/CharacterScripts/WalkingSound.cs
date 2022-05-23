using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    bool isMoving = false;
    bool canPlay = true;
    int moveIndex = 0;
    float waitTime;
    ThirtPersonPLayerScript parent;
    AudioSource walkLeft;
    AudioSource walkRight;

    //Sets all the correct references
    void Start()
    {
      parent = gameObject.transform.parent.GetComponent<ThirtPersonPLayerScript>();
      walkLeft = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
      walkRight = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      IsPlayerMoving();
      PlaySound();
    }

    //checks if the player is moving
    void IsPlayerMoving(){
      if((parent.horizontalMove != 0 || parent.vertical != 0) && parent.isGrounded){
        if(Input.GetKey(KeyCode.LeftShift) && parent.vertical > 0){
          waitTime = 0.3f;
        } else {
          waitTime = 0.5f;
        }
        
        isMoving = true;
      } else {
        isMoving = false;
      }
    }

    //Plays the sound
    void PlaySound(){
      if(isMoving && canPlay){
        if(!walkLeft.isPlaying || !walkRight.isPlaying){
          canPlay = false;
          StartCoroutine(WaitForReset());
          switch(moveIndex){
            case 0:
              walkLeft.Play();
              moveIndex = 1;
              break;
            case 1:
              walkRight.Play();
              moveIndex = 0;
              break;
          }
        }
      }
    }

    IEnumerator WaitForReset(){
      yield return new WaitForSeconds(waitTime);
      canPlay = true;
    }
}
