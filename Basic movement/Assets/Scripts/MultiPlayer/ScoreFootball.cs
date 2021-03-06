using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFootball : MonoBehaviour
{

    public GameObject confeti;
    public GameObject confetiFiveSeconds;
    public GameObject windmill;
    public GameObject solarPanel;
    public MultiPlayerHandler MPH;

    // Start is called before the first frame update
    void Start()
    {
        MPH = GameObject.FindObjectOfType<MultiPlayerHandler>();
        windmill = GameObject.Find("windmill (2)");
        solarPanel = GameObject.Find("solar-panel (4)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {
            MPH.GoalScored(other);

        }
    }

    public void SpawnConfeti(Collider other)
    {
        confetiFiveSeconds = Instantiate(confeti, new Vector3(111, 7, 360), Quaternion.Euler(90, 0, 0));
        Destroy(confetiFiveSeconds, 5);
        other.gameObject.transform.position = new Vector3(129.43f, 2.39f, 360.32f);
        windmill.SetActive(true);
        solarPanel.SetActive(true);
    }

}
