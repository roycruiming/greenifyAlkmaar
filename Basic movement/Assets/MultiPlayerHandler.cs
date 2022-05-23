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
    public Text gefeliciteerd;
    public GameObject bridge;
    public ScoreFootball1 sc1;
    public ScoreFootball sc;
    public GameObject football;
    public GameObject addingTreeAfterProgress;
    private int totalPlayerCount = 0;
    public bool player1IsSet = false;
    public bool player2IsSet = false;

    public HUDController hud;

    public GameObject player1;
    public GameObject player2;

    public GameObject flagPlayer1;
    public GameObject flagPlayer2;

    public Button mpBackButton;
    public GameObject medalIcon;



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
        }
        if (stream.IsReading)
        {
            solarCounter = (int)stream.ReceiveNext();
            TurbineCounter = (int)stream.ReceiveNext();
            treeCounter = (int)stream.ReceiveNext();
            totalCount = (int)stream.ReceiveNext();
        }
    }

    public bool isPlayer1Set() {
        int playerCount = this.GetTotalPlayerCount();
    
        if(playerCount >= 2) return true;
        else return false;
    }

    public int GetTotalPlayerCount() {
        int playerCount = 0;
        foreach(Player player in PhotonNetwork.PlayerList) playerCount++;
        return playerCount;
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
        gefeliciteerd = GameObject.Find("Gefeliciteerd").GetComponent<Text>();
        PhotonNetwork.InstantiateRoomObject("soccer-ball (3)", new Vector3(129.43f, 2.39f, 360.32f), Quaternion.identity);
        football = GameObject.Find("soccer-ball (3)");
        mpBackButton = GameObject.Find("mpExitButton").GetComponent<Button>();
        medalIcon = GameObject.Find("MedalIcon");


        totalTurbineCount = GameObject.FindGameObjectsWithTag("TurbineMultiplayer").Length;
        totalSolarCount = GameObject.FindGameObjectsWithTag("SolarMultiplayer").Length;
        totalTreeCount = GameObject.FindGameObjectsWithTag("TreeMultiplayer").Length;
        totalCounter = totalTurbineCount + totalSolarCount + totalTreeCount;

        player1 = GameObject.FindGameObjectWithTag("Mp1");
        player2 = GameObject.FindGameObjectWithTag("Mp2");

        gefeliciteerd.enabled = false;
        mpBackButton.gameObject.SetActive(false);
        medalIcon.SetActive(false);

        
        





    }

    // Update is called once per frame  
    void Update()
    {
        player1 = GameObject.FindGameObjectWithTag("Mp1");
        player2 = GameObject.FindGameObjectWithTag("Mp2");
        solarCounterText.text = solarCounter.ToString() + "/" + totalSolarCount;
        turbineCounterText.text = TurbineCounter.ToString() + "/" + totalTurbineCount;
        treeCounterText.text = treeCounter.ToString() + "/" + totalTreeCount;
        totalCounterText.text = totalCount.ToString() + "/" + totalCounter;

        totalCount = solarCounter + TurbineCounter + treeCounter;

        if (Input.GetKey(KeyCode.Backspace)){
            WinConditie(); 
        }

        if (Input.GetKeyDown("k"))
        {

            photonView.RPC("test", RpcTarget.All);
        }

        if (brScript.bridgeUps)
        {
            photonView.RPC("bridgeActivate", RpcTarget.All);
            //bridgeActivate();
        }
        else
        {
            photonView.RPC("bridgeDeActivate", RpcTarget.All);
            //bridgeDeActivate();
        }

        if (Input.GetKeyDown("h"))
        {
            print("H is pressed!");

            if (photonView.IsMine)
            {
                photonView.RPC("CallFriend2", RpcTarget.All);
                GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("friend is called"));
            }
            if (!photonView.IsMine)
            {
                photonView.RPC("CallFriend1", RpcTarget.All);
                GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("friend is called"));
            }

        }

        if (Input.GetKeyDown("g"))
        {
            print("G is pressed!! ");
            if (photonView.IsMine)
            {
                photonView.RPC("PlaceFlagPlayer1", RpcTarget.All);

            }
            if (!photonView.IsMine)
            {
                photonView.RPC("PlaceFlagPlayer2", RpcTarget.All);
            }
        }


        //check if a new player has joined and set activate his intro cutscene
        // if(this.totalPlayerCount != this.GetTotalPlayerCount()) {
        //     this.totalPlayerCount = this.GetTotalPlayerCount();

        //     if(totalPlayerCount == 1) {
        //         //activate for player 1
        //         StartCoroutine(showcaseIntroCutscene("Mp1"));
        //     }
        //     else {
        //         //activate for player 2
        //         StartCoroutine(showcaseIntroCutscene("Mp2"));
        //     }
        // }


        if(totalCount == totalCounter)
        {

            photonView.RPC("WinConditie", RpcTarget.All);
        }

    }

    IEnumerator showcaseIntroCutscene(string playerTag) {
        
        yield return null;
        // this.mainCamera.SetActive(false);
        // this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(true);
        // yield return new WaitForSeconds(1);
        // GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(null,null,GlobalGameHandler.GetSentencesByDictionaryKey("intro cheesemarket"));
        // yield return new WaitForSeconds(18);

        // this.cutsceneParent.transform.Find("introCutscene").gameObject.SetActive(false);
        // this.mainCamera.SetActive(true);
        // yield return null;
        
    }

    [PunRPC]
    public void WinConditie()
    {
        Cursor.lockState = CursorLockMode.None;
        gefeliciteerd.enabled = true;
        mpBackButton.gameObject.SetActive(true);
        medalIcon.SetActive(true);
        Time.timeScale = 0f;
    }

    [PunRPC]
    [System.Obsolete]
    public void PlaceFlagPlayer1()
    {
        print("In functie van G");
        if(flagPlayer1 == null)
        {
            flagPlayer1 = PhotonNetwork.InstantiateRoomObject("flag-big", player1.transform.position, Quaternion.identity);

        }
        else
        {
            PhotonNetwork.Destroy(flagPlayer1);
            flagPlayer1 = PhotonNetwork.InstantiateRoomObject("flag-big", player1.transform.position, Quaternion.identity);
            //flagPlayer1.transform.position = transform.position + new Vector3(player1.transform.position.x, 0, player1.transform.position.z);

        }
    }
    [PunRPC]
    [System.Obsolete]
    public void PlaceFlagPlayer2()
    {
        if(flagPlayer2 == null)
        {
            flagPlayer2 = PhotonNetwork.InstantiateRoomObject("flag-big", player2.transform.position, Quaternion.identity);

        }
        else
        {
            PhotonNetwork.Destroy(flagPlayer2);
            flagPlayer2 = PhotonNetwork.InstantiateRoomObject("flag-big", player2.transform.position, Quaternion.identity);
            //flagPlayer2.transform.position = transform.position + new Vector3(player2.transform.position.x, 0, player2.transform.position.z);
        }
    }

    [PunRPC]
    public void CallFriend1()
    {
        print("In functie van H");
        if (photonView.IsMine)
        {
            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("ask for help multiplayer"));

        }

        
    }

    [PunRPC]
    public void CallFriend2()
    {
        print("In functie van H");

        if (!photonView.IsMine)
        {
            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey("ask for help multiplayer"));

        }
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
