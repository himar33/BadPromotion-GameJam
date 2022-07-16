using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    void Start()
    {
        Color[] colors = new Color[4];
        colors[0] = Color.red - new Color(0.4f, 0.4f, 0.4f);
        colors[1] = Color.green - new Color(0.2f, 0.2f, 0.2f);
        colors[2] = Color.blue - new Color(0.2f, 0.2f, 0.2f);
        colors[3] = Color.yellow - new Color(0.2f, 0.2f, 0.2f);

        Color col = colors[Random.Range(0, 4)];
        MeshRenderer ren = GetComponent<MeshRenderer>();
        ren.material.color = col;

        Outline outline = GetComponent<Outline>();
        outline.OutlineColor = col + new Color(0.4f, 0.4f, 0.4f, 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
    }
}
