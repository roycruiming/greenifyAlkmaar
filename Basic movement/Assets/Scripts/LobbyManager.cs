using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Linq;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    
    private string levelName = "AZ-Stadion";

    //max players per room
    public byte maxPlayers;

    public void CreateButton()
    {


        if (createInput.text != "")
        {
            //Sends the analytics that multiplayer has started
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
             "LevelStart",
            new Dictionary<string, object> {
            {"level", levelName}
            });


            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers;

            PhotonNetwork.CreateRoom(createInput.text, roomOptions);
        }
        else
        {
            createInput.text = "Enter room Name";
        }

    }
    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Multiplayer");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("LobbyScreen");
    
    }

    public void InviteButton() {
        //GlobalGameHandler x = GlobalGameHandler.GetInstance();

        SceneManager.LoadScene("InviteFriends");

        

        
    }
}
