using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    //Vector3 vel = Vector3.zero;
    //Vector3 offset = new Vector3(0, 0, 10);
    float vel = 0f;

    public float smoothTime = 0.3f;
    float z = 0f;
    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 target = player.transform.position + offset;
        //transform.position = Vector3.SmoothDamp(target, transform.position, ref vel, smoothTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, z);

        float x = Mathf.SmoothDamp(player.transform.position.x, transform.position.x, ref vel, 1.8f);
        float originY = transform.position.y + player.transform.position.y;
        float y = Mathf.SmoothDamp(originY * 0.4f, transform.position.y, ref vel, 0.5f);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
