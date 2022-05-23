using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThirtPersonPLayerScript : MonoBehaviour
{
    public float walkSpeed, runSpeed, rotateSpeed, jumpForce, walkAnimationSpeed, runAnimatonSpeed;

    public Animator animator;
    public new Rigidbody rigidbody;

    Vector3 offset;

    public float distToGround;

    public bool isGrounded;


    private float m_currentH = 0;
    public float horizontalMove;
    public float vertical;

    private readonly float m_interpolation = 10;

    [SerializeField] private float m_turnSpeed = 200;


    public PhotonView view;
    public Camera cam;

    public bool isJumping;
    private bool isGrounded1;

    public HUDController hud;
    public MultiPlayerHandler mp;
    private bool playerMovementDisabled = false;

    private bool doubleJumpCounter = false;


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


        mp = GameObject.FindObjectOfType<MultiPlayerHandler>();
    }

    public void TogglePlayerMovementDisabled(bool forcedState = false, bool isDisabled = false) {
        if(forcedState) playerMovementDisabled = isDisabled;
        else playerMovementDisabled = !playerMovementDisabled;
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            if(GetComponent<Outline>() !=  null) GetComponent<Outline>().enabled = false;
        }

        if (!PauseMenu.GameIsPaused && !PuzzleController.PuzzlePlaying && view.IsMine && playerMovementDisabled != true)
        {


            Debug.DrawRay(transform.position, -Vector3.up, Color.red);

            isGrounded = Grounded();
            if (isGrounded && !isJumping)
            {
                GetComponent<Animator>().Play("Blend Tree");
                StartCoroutine(WaitForSeconds(0.5f));
            }
            if(!isGrounded)
            {

                GetComponent<Animator>().Play("falling");
                StartCoroutine(WaitForSeconds(0.5f));

            }
            

            //Allow the player to move left and right
            horizontalMove = Input.GetAxisRaw("Horizontal");
            //Allow the player to move forward and back
            vertical = Input.GetAxisRaw("Vertical");

            float speed = walkSpeed;
            float animSpeed = walkAnimationSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(!Input.GetKey(KeyCode.S))
                {
                    vertical *= 2f;
                    speed = runSpeed;
                    animSpeed = runAnimatonSpeed;
                }
            }

            var translation = transform.forward * (vertical * Time.deltaTime);
            translation += transform.right * (horizontalMove * Time.deltaTime);
            translation *= speed;
            translation = rigidbody.position + translation;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {

                    doubleJumpCounter = true;

                    GetComponent<Animator>().Play("jumping");

                    //isGrounded = false;
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    StartCoroutine(WaitForSeconds(1.4f));

                


            }


            float horizontal = Input.GetAxis("Mouse X");

            m_currentH = Mathf.Lerp(m_currentH, horizontal, Time.deltaTime * m_interpolation);

            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);


            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);

            animator.SetFloat("WalkSpeed", animSpeed);

            rigidbody.MovePosition(translation);


/*
            if (GameObject.Find("MultiPlayerHandler") != null){

                if (Input.GetKeyDown("h")){

                    mp.CallFriend();
                }
            }*/

        }
    }

        bool Grounded()
        {
        //StartCoroutine(WaitForSeconds());
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
        }



    IEnumerator WaitForSeconds(float time)
    {
        isJumping = true;
        yield return new WaitForSeconds(time);
        isJumping = false;
    }


}
