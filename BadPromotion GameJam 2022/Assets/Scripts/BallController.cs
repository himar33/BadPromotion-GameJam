using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField ]Rigidbody rb;
    public GameObject player;
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
        //rb.AddTorque(new Vector3(0, 0, 1), ForceMode.Force);

    }

    // Update is called once per frame
    void Update()
    {
        //bool touching = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        

        Vector3 p = new Vector3(collider.center.x + collider.radius, collider.center.y + collider.radius, collider.center.z + collider.radius);
        float d = Vector3.Distance(p, player.transform.position);
        //float d = p.x - player.transform.position.x;
        Debug.Log(d);
        if (Mathf.Abs(d) <= 52)
        {
            playerDir = playerController.dir.x;
            rb.AddTorque(new Vector3(0, 0, 2 * -playerDir), ForceMode.Force);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //rb.AddTorque(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
            rb.AddTorque(new Vector3(0, 0, 10 * -playerDir), ForceMode.Force);
        }
    }

}
