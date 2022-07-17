using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChecker : MonoBehaviour
{
    public BallController ball;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ball.allowMove = true;
            //ball.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ball.allowMove = false;
            //ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}
