using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterControllerScript : MonoBehaviour
{

    //movement
    private float horizontal;
    private float vertical;
    private Vector3 moveInput;
    private Vector3 moveDirection;
    public float moveSpeed;
    private float idleSpeed;
    private CharacterController controller;
    private Vector3 Velocity;
    public float jumpForce;
    public pausescript pause;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    //look variables
    public float sensX;
    public float sensY;
    private Camera cam;
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

    //sounds
    private float footTiming = 1f;
    public AudioSource footsteps;
    public AudioClip left;
    public AudioClip right;
    bool isStepping = false;
    private float lastFootstepTime = 0f;

    void Start()
    {
        cam = Camera.main;
        cursorDisable();
        controller = GetComponent<CharacterController>();
        idleSpeed = moveSpeed;
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
        if(pause.isPaused == false)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        

        //walking sounds
        if((vertical > 0.1 || horizontal > .1 || vertical < -0.1 || horizontal < -0.1) && controller.isGrounded == true)
        {
            Debug.Log(horizontal);
            footstepSound();
        }

        //crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (controller.height == 2)
            {
                footTiming = .75f;
                controller.height = 1;
                moveSpeed /= 2;
            }
            else
            {

                footTiming = .5f;
                controller.height = 2;
                moveSpeed *= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && c4!=null && c4.activeSelf == false)
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

        //sprinting
        if (Input.GetKey(KeyCode.LeftShift) && controller.height != 1)
        {
            moveSpeed = idleSpeed * 1.5f;
            footTiming = .25f;
        }
        else
        {
            if(moveSpeed > idleSpeed)
            {
                moveSpeed = idleSpeed;
            }
            if(controller.height != 1 && footTiming != .5f)
            {
                footTiming = .5f;
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
        float sub = 400 * (damage * .01f);
        healthBar.transform.localScale -= new Vector3((sub / 100), 0, 0);

        Debug.Log("hit for " + damage);
        health -= damage;
        int healthint = (int)health;
        healthText.text = healthint.ToString();
        if(health <= 0)
        {
            GameObject.Find("Canvas").GetComponent<DeathMenu>().Die();
        }
    }
    
    void footstepSound()
    {
        if (vertical >= 0.1f || horizontal >= .1f || vertical <= -0.1f || horizontal <= -0.1f)
        {
            
            // Play footsteps sound effect
            if (Time.time > lastFootstepTime + footTiming)
            {
                if (footsteps.clip == left)
                {
                    footsteps.clip = right;
                    footsteps.Play();
                }
                else
                {
                    footsteps.clip = left;
                    footsteps.Play();
                }
                lastFootstepTime = Time.time;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "web")
        {
            Destroy(other.gameObject);
            Damage(20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
