using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerHandler : MonoBehaviourPunCallbacks, IPunObservable
{
    public int counter;
    public Text scherm;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(counter);
        }
        if (stream.IsReading)
        {
            counter = (int)stream.ReceiveNext();
        }
    }

    //public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        //view = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        scherm.text = counter.ToString();

        if (Input.GetKeyDown("k")){
            counter++;
            //view.RPC("CounterUp()", RpcTarget.All);
            print("Neeeeeee");
        }

        
    }

/*    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(counter);
        }
        if (stream.IsReading)
        {
            counter = (int)stream.ReceiveNext();
        }
    }*/


/*    [PunRPC]
    public void CounterUp()
    {
        print("JAAAAAAA");
        counter++;  
    }
    */

}
