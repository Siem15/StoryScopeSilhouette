using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JZTurnSpriteCounter : MonoBehaviour
{
    public int DegreesToAction;
    float lastDegree;
    bool resetDegrees = true;
    FiducialController fiducialController;
    void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    void Update()
    {
        if (fiducialController.IsVisible)
        {
            if (resetDegrees)
            {
                resetDegrees = false;
                lastDegree = transform.localEulerAngles.z;
            }
            if (transform.localEulerAngles.z > lastDegree + DegreesToAction)
            {
                lastDegree = transform.localEulerAngles.z;
                X(-1);
            }
            if (transform.localEulerAngles.z < lastDegree - DegreesToAction)
            {
                lastDegree = transform.localEulerAngles.z;
                 X(1);
            }
        }
        if (!fiducialController.IsVisible && !resetDegrees)
        {
            resetDegrees = true;
        }
    }

    public void X(int i)
    {
        JZSpriteCounter.spriteCounter = Mathf.Clamp(JZSpriteCounter.spriteCounter += i,0,100);
        //print(JZSpriteCounter.spriteCounter);
    }
}

public static class JZSpriteCounter
{
    [Range(0, 100)]
    public static int spriteCounter;
}
