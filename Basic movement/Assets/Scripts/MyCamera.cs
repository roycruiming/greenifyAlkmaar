using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MyCamera : MonoBehaviour
{
/*    public float cameraSmoothingFactor = 1;
    public float lookUpMax;
    public float lookUpMin;


    private Quaternion camRotation;

*/


    public PhotonView view;

    [Header("Positioning")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject rotationAnkerObject;
    [SerializeField] private Vector3 translationOffset;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float maxViewingAngel;
    [SerializeField] private float minViewingAngel;
    [SerializeField] private float rotationSensitivity;



    private float verticalRotationAngle;

    public Vector3 FollowOffset
    {
        get
        {
            return followOffset;
        }
    }

    private void Awake()
    {

    }

    void Start()
    {

        //camRotation = transform.localRotation;
    }

    void FixedUpdate()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();

        if (!PauseMenu.GameIsPaused && !PuzzleController.PuzzlePlaying)
        {
            /*            camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1);

                        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

                        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);*/


            //Make the camera look at the target
            float yAngle = target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, yAngle, 0);

            transform.position = target.transform.position - (rotation * followOffset);
            transform.LookAt(target.transform.position + translationOffset);

            //Make the camera look up or down
            verticalRotationAngle += Input.GetAxis("Mouse Y") * rotationSensitivity;
            if (verticalRotationAngle > maxViewingAngel)
            {
                verticalRotationAngle = maxViewingAngel;
            }
            else if (verticalRotationAngle < minViewingAngel)
            {
                verticalRotationAngle = minViewingAngel;
            }

            transform.RotateAround(rotationAnkerObject.transform.position, rotationAnkerObject.transform.right, -verticalRotationAngle);



        }
        else
        {

        }


    }
}
