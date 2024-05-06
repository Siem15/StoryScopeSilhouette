using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RestartUnity : MonoBehaviour
{
    bool pressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && !pressed)
        {
            System.Diagnostics.Process.Start("/home/light.sh");
            Application.Quit();
        }
    }
}