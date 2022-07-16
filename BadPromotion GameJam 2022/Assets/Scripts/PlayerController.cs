using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]

    [Space]

    [Header("Speeds")]
    [SerializeField] private float rollTime = 20f;
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 4.0f;
    [SerializeField] private float ballSpeed = 6.0f;
    [SerializeField] private float superBallSpeed = 8.0f;
    [SerializeField] private float ultraBallSpeed = 10.0f;

    [Space]

    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private int life = 50;
    [SerializeField] private int damage = 5;
    [SerializeField] private int collectables = 0;
    [SerializeField] public TMP_Text collectablesText;

    [Space]

    [Header("Collider info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask ground;

    [Space]

    [Header("Token stats")]
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private float tokenVelocity;

    [Space]

    [Header("Audio")]
    [SerializeField] private AudioClip[] sfxClips;

    private CharacterController characterController;
    private AudioManager audio;
    private float currPlayerSpeed;
    private PlayerState state;
    private float currRollTime;
    public bool isGrounded;
    private Vector3 dir;
    private float chargeTime;


    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioManager>();
        state = PlayerState.MOVE;
        currPlayerSpeed = walkSpeed;
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
        if(collectablesText != null) collectablesText.text = collectables.ToString();
    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        if (isGrounded && dir.y < 0)
        {
            dir.y = 0f;
        }

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currRollTime < rollTime * 3) currRollTime++;

            if (currRollTime < rollTime)
            {
                currPlayerSpeed = runSpeed;
            }
            else if (currRollTime > rollTime && currRollTime < rollTime * 2)
            {
                currPlayerSpeed = ballSpeed;
            }
            else if (currRollTime > rollTime * 2 && currRollTime < rollTime * 3)
            {
                currPlayerSpeed = superBallSpeed;
            }
            else if (currRollTime >= rollTime * 3)
            {
                currPlayerSpeed = ultraBallSpeed;
            }
        }
        else
        {
            currRollTime = 0;
            currPlayerSpeed = walkSpeed;
        }

        dir.x = Input.GetAxis("Horizontal") * currPlayerSpeed;
    }

    private void FixedUpdate()
    {
        dir.y += gravityValue * Time.fixedDeltaTime;

        characterController.Move(dir * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectable")
        {
            audio.PlayClip(sfxClips[0]);
            collectables++;
            Destroy(other.gameObject);
        }
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
