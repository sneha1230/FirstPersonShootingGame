using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController characterController;
    Vector3 moveInput;
    public Transform cameraTran;
    public float mouseSensitivity;
    public bool invertX, invertY;
    Vector2 mouseInput;
    public float gravity;
    public float jumpForce;
    public bool canJump;
    public Transform groundCheckPoint;
    public LayerMask groundMask;
    bool canDoubleJump;
    public float runSpeed;
    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // moveInput.x = Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
        // moveInput.z = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;
        Vector3 horzMove = transform.right * Input.GetAxis("Horizontal");
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        moveInput = horzMove + vertMove;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }
        moveInput.y += Physics.gravity.y * gravity * Time.deltaTime;

        //jump code
        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, groundMask).Length>0;
        if (canJump)
        {
            canDoubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&canJump)
        {
            moveInput.y = jumpForce;
            canDoubleJump = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space)&&canDoubleJump)
        {
            moveInput.y = jumpForce;
            canDoubleJump = false;
        }
        characterController.Move(moveInput*Time.deltaTime);

        //camera rotation using mouse input
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*mouseSensitivity;
        if(invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        cameraTran.rotation = Quaternion.Euler(cameraTran.rotation.eulerAngles + new Vector3(mouseInput.y, 0f, 0f));

        //shooting the bullet
        if(Input.GetMouseButtonDown(0))
        {
            //using raycast
            RaycastHit hit;
            if(Physics.Raycast(cameraTran.position,cameraTran.forward,out hit, 50.0f))
            {
                if(Vector3.Distance(cameraTran.position,hit.point)>2.0f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(cameraTran.position + (cameraTran.forward * 30.0f));
            }

            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("OnGround", canJump);
    }
}
