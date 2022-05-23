using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoadingMultiplayer : MonoBehaviourPunCallbacks
{

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leave Room");
            PhotonNetwork.Disconnect();
        }

        PhotonNetwork.ConnectUsingSettings();


    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        SceneManager.LoadScene("LobbyScreen");
    }
}
