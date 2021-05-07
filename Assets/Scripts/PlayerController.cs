using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Manager managerScript;
    
    private Vector3 velocity;

    bool isGrounded;

    private float speed = 7.5f;
    private float force;
    private float gravity = -9.81f;
    private float groundDistance = 0.9f;
    private float mouseSensitivity = 140f;
    private float rotationOnX;

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

        if (isGrounded && velocity.y < 0)
        {
            if (managerScript.visionIsActive)
            {
                velocity.y = -2f;
            }
            else
            {
                velocity.y = -9.8f;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move;

        if (managerScript.visionIsActive)       // Movement RealWorld scene
        {
            move = transform.right * x + mainCamera.forward * z;
        }
        else
        {
            move = transform.right * x + transform.forward * z;     // Movement GhostsWorld scene
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            force = 1.8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            force = 1f;
        }

        controller.Move(move * speed * force * Time.deltaTime);
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
