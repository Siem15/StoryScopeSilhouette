using UnityEngine;

/// <summary>
/// Why is this a script. like seriously, wtf. This is redundant as all hell.
/// 
/// - Siem Wesseling, 13/05/2024
/// </summary>

public class JZRunInBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
    }
}