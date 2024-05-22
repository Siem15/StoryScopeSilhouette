using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script reloads the scene currently in use. I feel like this scene is redundant and can be merged with other scene load scripts.
/// 
/// - Siem Wesseling, 13/05/2024
/// </summary>

public class JZReloadScene : MonoBehaviour
{
    FiducialController fiducialController;

    // Start is called before the first frame update
    void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fiducialController.IsVisible) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }        
    }
}