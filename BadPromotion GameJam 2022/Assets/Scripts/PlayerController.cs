using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    [Space]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask ground;

    private CharacterController characterController;
    private readonly PlayerState state;
    private bool isGrounded;
    private Vector3 dir;

    private void Awake()
    {
        if (TryGetComponent<CharacterController>(out characterController))
        {
            Debug.Log("Everything fine!");
        }
        else
        {
            Debug.Log("Need a Character Controller!");
        }
    }

    private void Update()
    {
        switch (state)
        {
            case PlayerState.MOVE:
                HandleMovement();
                break;
            case PlayerState.NO_MOVE:
                break;
            default:
                break;
        }
    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        if (isGrounded && dir.y < 0)
        {
            dir.y = 0f;
        }

        dir.x = Input.GetAxis("Horizontal") * playerSpeed;

        if (dir.x != 0)
            transform.forward = new Vector3(dir.x, 0, 0);

        if(isGrounded && Input.GetButtonDown("Jump"))
            dir.y += Mathf.Sqrt(jumpForce * -2f * gravityValue);
    }

    private void FixedUpdate()
    {
        dir.y += gravityValue * Time.fixedDeltaTime;

        characterController.Move(dir * Time.fixedDeltaTime);
    }
}