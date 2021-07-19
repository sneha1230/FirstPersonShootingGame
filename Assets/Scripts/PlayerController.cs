using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public CharacterController characterController;
    Vector3 moveInput;
    public Transform cameraTran;
    public float mouseSensitivity;
    public bool invertX, invertY;
    Vector2 mouseInput;
    public float gravity;
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
        moveInput = moveInput * moveSpeed * Time.deltaTime;
        moveInput.y += Physics.gravity.y * gravity * Time.deltaTime;
        characterController.Move(moveInput);

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
    }
}
