using Photon.Pun;
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
    public Text solarCounterText;
    public Text turbineCounterText;
    public GameObject bridge;
    public ScoreFootball1 sc1;
    public ScoreFootball sc;
    public GameObject football;

    public float y = 0;
    bool up = true;
    bool down = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(solarCounter);
            //stream.SendNext(football);
        }
        if (stream.IsReading)
        {
            solarCounter = (int)stream.ReceiveNext();
            //football = (GameObject)stream.ReceiveNext();
        }
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
        PhotonNetwork.InstantiateRoomObject("soccer-ball (3)", new Vector3(76,2,104.5f), Quaternion.identity);
        football = GameObject.Find("soccer-ball (3)");
        
    }

    // Update is called once per frame  
    void Update()
    {
        solarCounterText.text = solarCounter.ToString();
        turbineCounterText.text = TurbineCounter.ToString();

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

}
