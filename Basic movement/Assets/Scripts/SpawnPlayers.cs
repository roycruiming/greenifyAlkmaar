using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject dirArrow;
    // private Vector3 startingPosCamera;
    // private Quaternion startingRotationCamera;
    private void Start()
    {
        bool player1Active = GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().isPlayer1Set();
        
        GameObject player;
        if(player1Active == false) {
            player = PhotonNetwork.Instantiate("Mp1", spawnPoint.position, spawnPoint.rotation);
        }
        else {
            player = PhotonNetwork.Instantiate("Mp2", spawnPoint.position, spawnPoint.rotation);
        }

        dirArrow = GameObject.Find("DirectionalArrow");
        dirArrow.SetActive(false);

        //initiate intro cutscene
        // GameObject maincameraPlayer = player.transform.Find("Main Camera").gameObject;
        // if(maincameraPlayer != null) {
        //     startingPosCamera = maincameraPlayer.transform.position;
        //     startingRotationCamera = maincameraPlayer.transform.rotation;

           
            // player.GetComponent<ThirtPersonPLayerScript>().toggleCanMoveAndLookAround();
            // StartCoroutine(ResetCameraAfterCutsceneTime(maincameraPlayer, player));
        // }   
    }

    // IEnumerator ResetCameraAfterCutsceneTime(GameObject playerCamera, GameObject player) {
    //     yield return new WaitForSeconds(15);
    	
    //     playerCamera.transform.position = this.startingPosCamera;
    //     playerCamera.transform.rotation = this.startingRotationCamera;
    //     player.GetComponent<ThirtPersonPLayerScript>().toggleCanMoveAndLookAround();

            

    //     yield return null;
    // }


}
