using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class restartUnity : MonoBehaviour
{
    bool pressed = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && !pressed)
        {
            System.Diagnostics.Process.Start("/home/light.sh");
            Application.Quit();                     
        }
    }
}
