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
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        dirArrow = GameObject.Find("DirectionalArrow");
        dirArrow.SetActive(false);

        bool player1Active = GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().isPlayer1Set();
        
        if(player1Active == false) GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().setPlayer1(player);
        else GameObject.Find("MultiPlayerHandler").GetComponent<MultiPlayerHandler>().setPlayer2(player);
    }

}
