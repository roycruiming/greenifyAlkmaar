using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private Vector3 startingPosPlayer;
    public void Awake() {
        if( GameObject.FindWithTag("Player").gameObject != null) {
            GameObject playerObj = GameObject.FindWithTag("Player").gameObject;
            this.startingPosPlayer = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);
        }
        
    }

    public void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "3RD Person") {
            if(startingPosPlayer != null) {
                if( GameObject.FindWithTag("Player").gameObject != null) {
                    if(GameObject.FindWithTag("LevelController").GetComponent<KaasmarktLevel>() != null) GameObject.FindWithTag("LevelController").GetComponent<KaasmarktLevel>().showSplashEffectAndSound();
                    
                    GameObject.FindWithTag("Player").gameObject.transform.position = startingPosPlayer;
                }
            }
        }
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     print("in the water");
    //     if(startingPosPlayer != null) {
    //         if( GameObject.FindWithTag("Player").gameObject != null) {
    //             GameObject.FindWithTag("Player").gameObject.transform.position = startingPosPlayer;
    //         }
    //     }
    // }
}
