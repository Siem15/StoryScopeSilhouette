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
        StartCoroutine(SetScreenResolution());
    }

    private IEnumerator SetScreenResolution()
    {
        while (!correctResolution)
        {
            if (WidthAndHeight())
            {
                correctResolution = true;
            }

            yield return new WaitForSeconds(5);
            Screen.SetResolution(1920, 1080, true);
        }
    }

    public bool WidthAndHeight()
    {
        return Screen.currentResolution.width == 1920 && Screen.currentResolution.height == 1080;        
    }
}