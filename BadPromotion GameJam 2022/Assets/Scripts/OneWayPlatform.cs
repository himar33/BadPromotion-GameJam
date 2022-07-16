using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] PlayerController player;

    BoxCollider collider;
    Collider playerCollider;
    private float couldown = 0;
    private bool falling = false;
    

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        playerCollider = player.gameObject.GetComponent<Collider>();
        Physics.IgnoreCollision(collider, playerCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            falling = true;
        if (falling)
        {
            couldown += Time.deltaTime;
            if (couldown > 0.5f)
            {
                couldown = 0;
                falling = false;
            }
        }
        if(falling)
            Physics.IgnoreCollision(collider, playerCollider, true);
        else
        {
            if (!player.isGrounded/* && player.gameObject.transform.position.y >= transform.position.y + 0.1*/)
            {
                Physics.IgnoreCollision(collider, playerCollider, true);
            }
            else
            {
                Physics.IgnoreCollision(collider, playerCollider, false);
            }
        }        
    }
}
