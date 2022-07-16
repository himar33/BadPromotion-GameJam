using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTemperature : MonoBehaviour
{
    Light directional;
    public float temperature;
    // Start is called before the first frame update
    void Start()
    {
        directional = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        directional.colorTemperature = Mathf.PingPong(Time.time * 100, 18500) + 1500;
        temperature = directional.colorTemperature;
    }
}
