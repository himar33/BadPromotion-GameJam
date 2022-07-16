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

        Color col = colors[Random.RandomRange(0, 4)];
        MeshRenderer ren = GetComponent<MeshRenderer>();
        //Material mat = ren.GetComponent<Material>();
        ren.material.color = col;
        //ren.material.SetColor("Color", col);
        //mat.color = col;
        //t.shader = Shader.Find("Universal Render Pipeline/Lit");
        //mat.SetColor("_Color", col);


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
