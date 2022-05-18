using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject dirArrow;
    private void Start()
    {
        bool player1Active = GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().isPlayer1Set();
        
        GameObject player;
         
        if(player1Active == false) {
            player = PhotonNetwork.Instantiate("Mp1", spawnPoint.position, spawnPoint.rotation);
            GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().setPlayer1();
        }
        else {
            player = PhotonNetwork.Instantiate("Mp2", spawnPoint.position, spawnPoint.rotation);
            GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().setPlayer2();
        }

        
        dirArrow = GameObject.Find("DirectionalArrow");
        dirArrow.SetActive(false);

        
    }

}
