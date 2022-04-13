using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public float cameraSmoothingFactor = 1;
    public float lookUpMax;
    public float lookUpMin;


    private Quaternion camRotation;


    void Start()
    {
        this.gameObject.SetActive(false);
        camRotation = transform.localRotation;
    }

    void FixedUpdate()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();

        if (!PauseMenu.GameIsPaused && !CleanSolarPanelPuzzle.IsPlaying && !HowmanyDidYouSeePuzzle.IsPlaying && !TurnTheTurbnines.IsPlaying)
        {
            camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1);

            camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        }
        else
        {

        }


    }
}
