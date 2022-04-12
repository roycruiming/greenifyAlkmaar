using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoadingMultiplayer : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        SceneManager.LoadScene("LobbyScreen");
    }
}
