using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCityCharacterPicker : MonoBehaviour
{
    public void Awake() {
        //disable all character models.
        for(int i = 0; i < transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        int randomChar = Random.Range(0,transform.childCount);
        this.transform.GetChild(randomChar).gameObject.SetActive(true);
        Random.Range(0,4);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
