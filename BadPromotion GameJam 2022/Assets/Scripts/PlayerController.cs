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
    [SerializeField] private int life = 50;
    [SerializeField] private int damage = 5;

    [Space]

    [Header("Collider info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask ground;

    [Space]

    [Header("Token stats")]
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private float tokenVelocity;

    private CharacterController characterController;
    private readonly PlayerState state;
    private bool isGrounded;
    private Vector3 dir;
    private float chargeTime;

    private Animator anim;

    private void Awake()
    {
        if (TryGetComponent<CharacterController>(out characterController))
        {
            anim = GetComponent<Animator>();
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
        {
            //anim.Play("Idle");
            transform.forward = new Vector3(dir.x, 0, 0);
        }

        //anim.SetFloat("speed", dir.x);
        //anim.SetBool("movingRight", dir.x > 0 ? true : false);

        if(isGrounded && Input.GetButtonDown("Jump"))
            dir.y += Mathf.Sqrt(jumpForce * -2f * gravityValue);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 pos = transform.position;
            pos.x += transform.forward.x;
            GameObject token = Instantiate(tokenPrefab, pos, transform.rotation);
            token.GetComponent<Rigidbody>().velocity = new Vector3(tokenVelocity * transform.forward.x, 0, 0);
            Destroy(token, 2);
        }
        if(Input.GetMouseButton(1))
        {
            Attack();
        }

    }

    private void FixedUpdate()
    {
        dir.y += gravityValue * Time.fixedDeltaTime;

        characterController.Move(dir * Time.fixedDeltaTime);
    }


    public void Attack()
    {

    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
    }

    public float GetLife()
    {
        return life;
    }
}
