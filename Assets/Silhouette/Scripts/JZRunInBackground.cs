using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZRunInBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
    }
}