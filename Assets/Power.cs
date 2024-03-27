using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField] private KeyCode lightSwitch = KeyCode.Mouse1;

    private bool activated = true;

    private Light lamp;


    private void Awake()
    {
        lamp = GetComponent<Light>();
    }


    void Update()
    {
        if (Input.GetKeyDown(lightSwitch))
        {
            if (activated)
            {
                lamp.enabled = false;
                activated = false;
            }
            else if(!activated)
            {
                lamp.enabled = true;
                activated = true;
            }

        }
    }
}
