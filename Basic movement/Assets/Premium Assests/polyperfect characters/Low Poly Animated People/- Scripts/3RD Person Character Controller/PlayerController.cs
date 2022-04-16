using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Polyperfect.People
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed, runSpeed, rotateSpeed, jumpForce, walkAnimationSpeed, runAnimatonSpeed;

        public Animator animator;
        public new Rigidbody rigidbody;

        Vector3 offset;

        public float distToGround;

        public bool isGrounded;


        private float m_currentH = 0;

        private readonly float m_interpolation = 10;

        [SerializeField] private float m_turnSpeed = 200;


   
        public Camera cam;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            distToGround = GetComponent<Collider>().bounds.extents.y;
        }

        // Update is called once per frame
        void Update()
        {
            
            isGrounded = Grounded();

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
                print("JUMP");
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //float horizontal = Input.GetAxis("Mouse X") * Time.deltaTime;
            //Quaternion rotation = transform.rotation * Quaternion.Euler(0, horizontal * rotateSpeed, 0);

            float horizontal = Input.GetAxis("Mouse X");

            m_currentH = Mathf.Lerp(m_currentH, horizontal, Time.deltaTime * m_interpolation);

            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);


            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);

            animator.SetFloat("WalkSpeed", animSpeed);

            rigidbody.MovePosition(translation);
            //rigidbody.MoveRotation(rotation);
        }

        bool Grounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround, 9);
        }
    }
}
