using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject dirArrow;
    private Vector3 startingPosCamera;
    private Quaternion startingRotationCamera;
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

        //initiate intro cutscene
        GameObject maincameraPlayer = player.transform.Find("Main Camera").gameObject;
        if(maincameraPlayer != null) {
            startingPosCamera = maincameraPlayer.transform.position;
            startingRotationCamera = maincameraPlayer.transform.rotation;
            
            StartCoroutine(ResetCameraAfterCutsceneTime(player.transform.tag));
        }   

        
        dirArrow = GameObject.Find("DirectionalArrow");
        dirArrow.SetActive(false);

        
    }

    IEnumerator ResetCameraAfterCutsceneTime(string playerTagName) {
        yield return new WaitForSeconds(15);
            GameObject p = GameObject.FindGameObjectWithTag(playerTagName);
            GameObject maincameraPlayer = p.transform.Find("Main Camera").gameObject;
                if(maincameraPlayer != null) {
                    maincameraPlayer.transform.position = this.startingPosCamera;
                    maincameraPlayer.transform.rotation = this.startingRotationCamera;
                }
            

        yield return null;
    }


}
