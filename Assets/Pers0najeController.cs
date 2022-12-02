using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pers0najeController : MonoBehaviour
{
    #region patata
    private CharacterController controller;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;
    private Transform cam;
    private float currentVelocity;
    [SerializeField] private float shoothTime = 0.5f;
    [SerializeField] private Transform groundSensor;
    [SerializeField] private float sensorRadius = 0.2f;
    [SerializeField] private LayerMask gorundLayer;
    private Vector3 playerVelocity;
    [SerializeField] private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovimientoPlayer();
        Jump();
    }

    void MovimientoPlayer()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref currentVelocity, shoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
    }


    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, gorundLayer);

        if (playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    #endregion

    //En el freeLock ponemos en los dos el player
}
