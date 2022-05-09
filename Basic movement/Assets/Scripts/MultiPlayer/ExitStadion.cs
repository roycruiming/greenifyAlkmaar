using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitStadion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {


        }
        else
        {
            other.gameObject.transform.position = new Vector3(129, 1.5f, 322);
        }
    }
}
