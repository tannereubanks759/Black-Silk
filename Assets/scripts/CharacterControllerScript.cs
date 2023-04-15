using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{

    //movement
    private float horizontal;
    private float vertical;
    private Vector3 moveInput;
    private Vector3 moveDirection;
    public float moveSpeed;
    private CharacterController controller;
    private Vector3 Velocity;
    public float jumpForce;

    //look variables
    public float sensX;
    public float sensY;
    public Camera cam;
    float mouseX;
    float mouseY;
    float multiplier = .01f;
    float xRotation;
    float yRotation;

    //health
    public float health = 100f;

    //raycast
    Ray RayOrigin;
    RaycastHit HitInfo;
    public GameObject c4;


    void Start()
    {
        cursorDisable();
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {


        //movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * vertical + transform.right * horizontal;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Velocity.y = jumpForce;
        }
        else
        {
            Velocity.y -= -9.81f * -2f * Time.deltaTime;
        }
        controller.SimpleMove((moveDirection).normalized * moveSpeed);
        controller.Move(Velocity * Time.deltaTime);


        //mouse Looking
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (controller.height == 2)
            {
                controller.height = 1;
                moveSpeed /= 2;
            }
            else
            {
                controller.height = 2;
                moveSpeed *= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(RayOrigin, out HitInfo, 3f))
            {
                if(HitInfo.collider.gameObject.tag == "c4")
                {
                    Destroy(HitInfo.collider.gameObject);
                    c4.SetActive(true);
                }
            }
        }
    }

    public void cursorDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void cursorEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Damage(float damage)
    {
        health -= damage;
    }
}
