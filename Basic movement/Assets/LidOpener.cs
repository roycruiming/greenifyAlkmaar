using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidOpener : MonoBehaviour
{
    // Start is called before the first frame update

    public float TimeToOpen;
    public float DegreesToRotate;

    private void Awake()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);       
    }

    public IEnumerator Rotate()
    {     
            float time = 0f;
            while (time < TimeToOpen)
            {

                time += Time.deltaTime;
                float deg = DegreesToRotate / TimeToOpen;

                transform.Rotate(new Vector3(-(Time.deltaTime * deg), 0, 0));
 
            yield return null;
            }
    }
}
