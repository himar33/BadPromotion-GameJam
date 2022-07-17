using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    //public float distance;
    public float maxSpeed;
    public float speedMultiplier;
    public bool allowMove;

    PlayerController playerController;
    float playerDir;
    SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        playerController = player.GetComponent<PlayerController>();
        playerDir = playerController.dir.x;
        allowMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMove)
        {
            playerDir = playerController.dir.x;
            rb.AddTorque(new Vector3(0, 0, 2 * speedMultiplier * -playerDir), ForceMode.Force);
            Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        }
    }

}
