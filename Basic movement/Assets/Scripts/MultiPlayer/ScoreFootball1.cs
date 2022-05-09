using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFootball1 : MonoBehaviour
{

    public GameObject confeti;
    public GameObject confetiFiveSeconds;
    public GameObject solarPanel;
    public GameObject windmill;
    public MultiPlayerHandler MPH;

    // Start is called before the first frame update
    void Start()
    {
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
        solarPanel = GameObject.Find("solar-panel (4)");
        windmill = GameObject.Find("windmill (2)");
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
        confetiFiveSeconds = Instantiate(confeti, new Vector3(150.13f, 7.03f, 360.31f), Quaternion.Euler(90, 0, 0));
        Destroy(confetiFiveSeconds, 5);
        other.gameObject.transform.position = new Vector3(129.43f, 2.39f, 360.32f);
        solarPanel.SetActive(true);
        windmill.SetActive(true);
        
    }
}
