using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Resolution : MonoBehaviour
{
    bool correctResolution = false;

    private void Start()
    {
        // Set screen resolution of application.
        StartCoroutine(SetScreenResolution());
    }

    private IEnumerator SetScreenResolution()
    {
        while (!correctResolution)
        {
            if (Screen.currentResolution.width == 1920
                && Screen.currentResolution.height == 1080)
            {
                correctResolution = true;
            }

            yield return new WaitForSeconds(5.0f);
            Screen.SetResolution(1920, 1080, true);
        }
    }
}