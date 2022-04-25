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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(solarCounter);
        }
        if (stream.IsReading)
        {
            solarCounter = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bridge = GameObject.Find("bridge-road-hill");
        brScript = GameObject.FindObjectOfType<bridgeUp>();
        solarCounterText = GameObject.Find("SolarCounter").GetComponent<Text>();
        turbineCounterText = GameObject.Find("WindCounter").GetComponent<Text>();
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

}
