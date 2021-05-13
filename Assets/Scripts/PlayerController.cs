using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Manager managerScript;
    
    private Vector3 velocity;

    bool isGrounded;

    private float speed = 11f;
    private float force;
    private float gravity = -9.81f;
    private float groundDistance = 0.9f;
    private float mouseSensitivity = 100f;
    private float rotationOnX = 0f;

    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        managerScript = GameObject.Find("Manager").GetComponent<Manager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        force = 1f;
    }

    void Update()
    {
        Movement_RealWorld();
        Rotation();
    }

    private void Movement_RealWorld()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move;

        if (isGrounded && velocity.y < 0)
        {
            if (managerScript.visionIsActive)
            {
                if (x > 0 || z > 0)
                    gravity = 0;
                else
                    gravity = -9.81f;

                velocity.y = -2f;
            }
            else if (!managerScript.visionIsActive)
            {
                velocity.y = -9.8f;
            }
        }

        if (managerScript.visionIsActive)       // Movement RealWorld scene
        {
            move = transform.right * x + mainCamera.forward * z * 1.5f;
        }
        else
        {
            move = transform.right * x + transform.forward * z;     // Movement GhostsWorld scene
        }

        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            force = 1.8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            force = 1f;
        }*/

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void Rotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        float m_X = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
        mainCamera.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);

        transform.Rotate(Vector3.up * m_X);
    }
}
