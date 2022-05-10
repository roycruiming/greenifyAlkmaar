using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleChildsHandler : MonoBehaviour
{
    public void Awake() {
        for(int i = 0; i < this.transform.childCount; i++) this.transform.GetChild(i).GetComponent<MeshRenderer>().material = Resources.Load("Materials/Invincible_Barrier") as Material;
        
    }
}
