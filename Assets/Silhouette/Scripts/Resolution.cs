using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    bool correctResolution = false;

    void Start()
    {
        StartCoroutine(SetScreenResolution());
    }


    IEnumerator SetScreenResolution()
    {
        while (!correctResolution)
        {
            if (WidthHeight())
            {
                correctResolution = true;
            }
            yield return new WaitForSeconds(5);
            Screen.SetResolution(1920, 1080, true);
        }
    }

    public bool WidthHeight()
    {
        if (Screen.currentResolution.width == 1920 && Screen.currentResolution.height == 1080)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
