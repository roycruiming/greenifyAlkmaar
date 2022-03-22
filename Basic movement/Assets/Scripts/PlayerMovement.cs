using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float range = 10f;
    public Transform dest;

    public float moveSpeed;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    public GameObject projectile;
    public Vector3 firingPointOffset = new Vector3(1, 1, 1);

    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;


    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;

        //jump
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    void Update()
    {
        // wasd movement
        float movement = Time.deltaTime * moveSpeed;

        float x = Input.GetAxis("Horizontal") * movement;
        float y = Input.GetAxis("Vertical") * movement;

        transform.Translate(new Vector3(x, 0f, y));

        // mouse turning
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);



        //jumping

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // shoot bullets
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(projectile, transform.position + firingPointOffset, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
        }*/

        // restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            moveSpeed = 5f;
        } 

        //Debug.DrawLine(transform.position, dest.position);


    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }


}
