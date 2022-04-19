using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//[RequireComponent(typeof(Rigidbody))]

public class ThirtPersonPLayerScript : MonoBehaviour
{
    public float walkSpeed, runSpeed, rotateSpeed, jumpForce, walkAnimationSpeed, runAnimatonSpeed;

    public Animator animator;
    public new Rigidbody rigidbody;

    Vector3 offset;

    public float distToGround = 3f;

    public bool isGrounded;


    private float m_currentH = 0;

    private readonly float m_interpolation = 10;

    [SerializeField] private float m_turnSpeed = 200;


    public PhotonView view;
    public Camera cam;

    private bool isJumping;
    private bool isGrounded1;


    public float cameraSmoothingFactor = 0.5f;
    public float lookUpMax = 10f;
    public float lookUpMin = -10f;

    private Quaternion camRotation;

    private void Awake()
    {

         if (!animator) { gameObject.GetComponent<Animator>(); }
         if (!rigidbody) { gameObject.GetComponent<Animator>(); }

        if (!view.IsMine && GetComponent<ThirtPersonPLayerScript>() != null)
        {

            //Destroy(GetComponent<ThirtPersonPLayerScript>());
            //Destroy(GetComponent<raycaster>());
            //Destroy(GetComponent<DirectionalArrow>());


        }
        if (!view.IsMine)
        {
            Destroy(cam);
        }

    }

        // Start is called before the first frame update
        void Start()
        {
        Cursor.lockState = CursorLockMode.Locked;
            view = this.GetComponent<PhotonView>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            distToGround = GetComponent<Collider>().bounds.extents.y;
        }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {


            Debug.DrawRay(transform.position, -Vector3.up, Color.red);

            isGrounded = Grounded();
            if (isGrounded)
            {
                animator.SetBool("IsGrounded", true);
                animator.SetBool("IsJumping", false);
                isJumping = false;
                animator.SetBool("IsFalling", false);
            }
            else
            {
                animator.SetBool("IsGrounded", false);


            }

            //Allow the player to move left and right
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            //Allow the player to move forward and back
            float vertical = Input.GetAxisRaw("Vertical");

            float speed = walkSpeed;
            float animSpeed = walkAnimationSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                vertical *= 2f;
                speed = runSpeed;
                animSpeed = runAnimatonSpeed;
            }

            var translation = transform.forward * (vertical * Time.deltaTime);
            translation += transform.right * (horizontalMove * Time.deltaTime);
            translation *= speed;
            translation = rigidbody.position + translation;

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                animator.SetBool("IsJumping", true);
                isJumping = true;
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            }


            float horizontal = Input.GetAxis("Mouse X");

            m_currentH = Mathf.Lerp(m_currentH, horizontal, Time.deltaTime * m_interpolation);

            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);


            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);

            animator.SetFloat("WalkSpeed", animSpeed);

            rigidbody.MovePosition(translation);
            //rigidbody.MoveRotation(rotation);

            camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1);

            camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

            cam.transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        }
    }

        bool Grounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround);
        }
    }



