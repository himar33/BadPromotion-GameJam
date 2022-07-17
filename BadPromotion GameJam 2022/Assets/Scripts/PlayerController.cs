using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private bool doubleJump = false;
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
    [SerializeField] private float shootRate = 1;
    private float couldownAttack;
    private bool onAttack = false;

    [Space]

    [Header("Audio")]
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private AudioClip[] jumpSFX;
    [SerializeField] private AudioClip[] tokenSFX;
    [SerializeField] private AudioClip[] stepsSFX;
    private AudioSource rollingAudio;

    private CharacterController characterController;
    private AudioManager audio;
    private float currPlayerSpeed;
    private PlayerState state;
    private float currRollTime;
    public bool isGrounded;
    public Vector3 dir;
    private float chargeTime;
    private float posZ;


    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioManager>();
        state = PlayerState.MOVE;
        currPlayerSpeed = walkSpeed;
        rollingAudio = transform.GetChild(3).GetComponent<AudioSource>();
        posZ = transform.position.z;
    }

    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

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
        if (onAttack)
        {
            couldownAttack += Time.deltaTime;
            if (couldownAttack > shootRate)
            {
                couldownAttack = 0;
                onAttack = false;
            }
        }

    }

    private void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded) doubleJump = false;

        if (isGrounded && dir.y < 0)
        {
            dir.y = 0f;
        }

        if (dir.x != 0)
        {
            transform.forward = new Vector3(dir.x, 0, 0);
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                dir.y += Mathf.Sqrt(jumpForce * -2f * gravityValue);
                audio.PlayRandomClip(false, jumpSFX);
            }
            else if (!doubleJump)
            {
                doubleJump = true;
                dir.y = Mathf.Sqrt(jumpForce * -1.5f * gravityValue);
                audio.PlayRandomClip(false, jumpSFX);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !onAttack)
        {
            onAttack = true;
            audio.PlayRandomClip(false, tokenSFX);
            Vector3 pos = transform.position;
            pos.x += transform.forward.x;
            GameObject token = Instantiate(tokenPrefab, pos, transform.rotation);
            token.GetComponent<Rigidbody>().velocity = new Vector3(tokenVelocity * transform.forward.x, 0, 0);
            Destroy(token, 3);
        }

        if(Input.GetMouseButton(1))
        {
            Attack();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!rollingAudio.isPlaying)
            {
                rollingAudio.Play();
            }

            if (currRollTime < rollTime * 3) currRollTime++;

            anim.SetBool("isRolling", true);

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
            rollingAudio.Stop();
            anim.SetBool("isRolling", false);
            currRollTime = 0;
            currPlayerSpeed = walkSpeed;
        }

        anim.SetFloat("speed", dir.x);
        anim.SetFloat("speedY", dir.y);
        anim.SetBool("isGrounded", isGrounded);

        dir.x = Input.GetAxis("Horizontal") * currPlayerSpeed;
    }

    private void FixedUpdate()
    {
        dir.y += gravityValue * Time.fixedDeltaTime;

        characterController.Move(dir * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            audio.PlayClip(sfxClips[0], false);
            collectables++;
            Destroy(other.transform.parent.gameObject);
        }
    }


    public void Attack()
    {

    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        Debug.Log(life);
        if (life <= 0)
            Die();
    }

    void Die()
    {
        // Play animation
    }

    public float GetLife()
    {
        return life;
    }

    public void StepHit()
    {
        audio.PlayRandomClip(false, stepsSFX);
    }
}
