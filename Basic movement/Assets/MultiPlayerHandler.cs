using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class MultiPlayerHandler : MonoBehaviourPunCallbacks, IPunObservable
{
    public bridgeUp brScript;
    public int solarCounter;
    public int TurbineCounter;
    public int treeCounter;
    public int totalSolarCount;
    public int totalTurbineCount;
    public int totalTreeCount;
    public int totalCount;
    public int totalCounter;
    public Text solarCounterText;
    public Text turbineCounterText;
    public Text treeCounterText;
    public Text totalCounterText;
    public GameObject bridge;
    public ScoreFootball1 sc1;
    public ScoreFootball sc;
    public GameObject football;
    public GameObject addingTreeAfterProgress;
    public bool player1IsSet = false;
    public bool player2IsSet = false;


    public float y = 0;
    bool up = true;
    bool down = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(solarCounter);
            stream.SendNext(TurbineCounter);
            stream.SendNext(treeCounter);
            stream.SendNext(totalCount);
            stream.SendNext(player1IsSet);
            stream.SendNext(player2IsSet);
        }
        if (stream.IsReading)
        {
            solarCounter = (int)stream.ReceiveNext();
            TurbineCounter = (int)stream.ReceiveNext();
            treeCounter = (int)stream.ReceiveNext();
            totalCount = (int)stream.ReceiveNext();
            player1IsSet = (bool)stream.ReceiveNext();
            player2IsSet = (bool)stream.ReceiveNext();
        }
    }

    public bool isPlayer1Set() {
        int playerCount = 0;
        foreach(Player player in PhotonNetwork.PlayerList) playerCount++;
        if(playerCount >= 2) return true;
        else return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        bridge = GameObject.Find("bridge-road-hill");
        sc1 = GameObject.FindObjectOfType<ScoreFootball1>();
        sc = GameObject.FindObjectOfType<ScoreFootball>();
        brScript = GameObject.FindObjectOfType<bridgeUp>();
        solarCounterText = GameObject.Find("SolarCounter").GetComponent<Text>();
        turbineCounterText = GameObject.Find("WindCounter").GetComponent<Text>();
        treeCounterText = GameObject.Find("TreeCounter").GetComponent<Text>();
        totalCounterText = GameObject.Find("TotalCounter").GetComponent<Text>();
        PhotonNetwork.InstantiateRoomObject("soccer-ball (3)", new Vector3(129.43f, 2.39f, 360.32f), Quaternion.identity);
        football = GameObject.Find("soccer-ball (3)");
        addingTreeAfterProgress = GameObject.Find("Adding");



        totalTurbineCount = GameObject.FindGameObjectsWithTag("TurbineMultiplayer").Length;
        totalSolarCount = GameObject.FindGameObjectsWithTag("SolarMultiplayer").Length;
        totalTreeCount = GameObject.FindGameObjectsWithTag("TreeMultiplayer").Length;
        totalCounter = totalTurbineCount + totalSolarCount + totalTreeCount;

        addingTreeAfterProgress.SetActive(false);




    }

    // Update is called once per frame  
    void Update()
    {
        solarCounterText.text = solarCounter.ToString() + "/" + totalSolarCount;
        turbineCounterText.text = TurbineCounter.ToString() + "/" + totalTurbineCount;
        treeCounterText.text = treeCounter.ToString() + "/" + totalTreeCount;
        totalCounterText.text = totalCount.ToString() + "/" + totalCounter;

        totalCount = solarCounter + TurbineCounter + treeCounter;

        if (Input.GetKeyDown("k"))
        {

            photonView.RPC("test", RpcTarget.All);
        }

        if (brScript.bridgeUps)
        {
            bridgeActivate();
        }
        else
        {
            bridgeDeActivate();
        }


        if(totalCount > 5)
        {
            AddTreesAfterObject();
        }


    }

    [PunRPC]
    public void test()
    {
        solarCounter++;
    }

    [PunRPC]
    public void bridgeActivate()
    {
        bridge.SetActive(true);
    }

    [PunRPC]
    public void bridgeDeActivate()
    {
        bridge.SetActive(false);
    }

    [PunRPC]
    public void CollectableSolarPanel()
    {
        solarCounter++;
    }

    [PunRPC]
    public void CollectableWindTurbine()
    {
        TurbineCounter++;
    }
    [PunRPC]
    public void CollectableTree()
    {
        treeCounter++;
    }

    [PunRPC]
    public void setPlayer1(GameObject go) {
        this.player1IsSet = true;
        go.transform.tag = "Mp1";
    }

    [PunRPC]
    public void setPlayer2(GameObject go) {
        this.player2IsSet = true;
        go.transform.tag = "Mp2";
    }




    [PunRPC]
    public void MoveVork()
    {
        if (y < 2f && up)
        {
            y += 0.01f;
        }
        if (y >= 2)
        {
            up = false;
            down = true;

        }
        if (y > 0f && down)
        {
            y -= 0.01f;
        }
        if (y <= 0)
        {
            up = true;
            down = false;
        }

    }

    [PunRPC]
    public void GoalScored1(Collider other)
    {
        sc1.SpawnConfeti1(other);
    }

    [PunRPC]
    public void GoalScored(Collider other)
    {
        sc.SpawnConfeti(other);
    }

    [PunRPC]
    public void AddTreesAfterObject()
    {
        addingTreeAfterProgress.SetActive(true);
    }

}
