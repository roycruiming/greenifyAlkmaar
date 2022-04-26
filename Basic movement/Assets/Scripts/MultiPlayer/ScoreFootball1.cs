using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFootball1 : MonoBehaviour
{

    public GameObject confeti;
    public GameObject confetiFiveSeconds;
    public GameObject solarPanel;
    public MultiPlayerHandler MPH;

    // Start is called before the first frame update
    void Start()
    {
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
        solarPanel = GameObject.Find("solar-panel (4)");
        solarPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {
            MPH.GoalScored1(other);

        }
    }

    public void SpawnConfeti1(Collider other)
    {
        confetiFiveSeconds = Instantiate(confeti, new Vector3(95, 6.8f, 103.5f), Quaternion.Euler(90, 0, 0));
        Destroy(confetiFiveSeconds, 5);
        other.gameObject.transform.position = new Vector3(76, 2.5f, 104);
        solarPanel.SetActive(true);
    }
}
