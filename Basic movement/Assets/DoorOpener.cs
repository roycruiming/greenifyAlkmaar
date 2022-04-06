using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float TimeToOpen;
    public float DegreesToRotate;

    [SerializeField] private bool NegativeAngle; 

    private void Awake()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator Open()
    {
        float time = 0f;
        while (time < TimeToOpen)
        {

            time += Time.deltaTime;
            float deg = DegreesToRotate / TimeToOpen;
            int angleMultiplier;

            if (NegativeAngle) { angleMultiplier = -1; }
            else { angleMultiplier = 1;  }


            transform.Rotate(new Vector3(0, (Time.deltaTime * deg * angleMultiplier), 0));

            yield return null;
        }
    }
}










