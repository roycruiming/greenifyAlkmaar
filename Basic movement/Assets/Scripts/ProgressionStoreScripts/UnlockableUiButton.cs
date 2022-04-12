using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnlockableUiButton : MonoBehaviour
{
    public int unlockableId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurchaseUnlockable() {
        print("Yoo");
        //GameObject.Find("ProgressionStoreHandler").GetComponent<ProgressionStoreHandler>().purchaseUnlockable(this.unlockableId);
    }
}
