using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectablesMultiplayer : MonoBehaviour
{
    public MultiPlayerHandler MPH;
    Collider m_ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();
        m_ObjectCollider.isTrigger = false;
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (this.gameObject.CompareTag("SolarMultiplayer"))
        {
            MPH.CollectableSolarPanel();
            Destroy(this.gameObject);
        }
        else if (this.gameObject.CompareTag("TurbineMultiplayer"))
        {
            MPH.CollectableWindTurbine();
            Destroy(this.gameObject);
        }
        else if (this.gameObject.CompareTag("TreeMultiplayer"))
        {
            MPH.CollectableTree();
            Destroy(this.gameObject);
        }

        //decide if a positive feedback message is going to be displayed to the player
        int oneToThreeChance = Random.Range(1,4);

        if(oneToThreeChance == 1) {
            //pick a random message
            int messageIndex = Random.Range(1,5);
            string key = "multiplayer positive feedback " + messageIndex;

            GameObject.Find("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(GlobalGameHandler.GetTextByDictionaryKey(key));
        }

    }
}
