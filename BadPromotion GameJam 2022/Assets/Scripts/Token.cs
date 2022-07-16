using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    void Start()
    {
        Color[] colors = new Color[4];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        colors[3] = Color.yellow;

        Color col = colors[Random.Range(0, 4)];
        MeshRenderer ren = GetComponent<MeshRenderer>();
        ren.material.color = col;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
    }
}
