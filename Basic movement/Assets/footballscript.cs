using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class footballscript : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transferOwnership();
    }

    [PunRPC]
    public void transferOwnership()
    {
        this.photonView.TransferOwnership(0);
    }
}
