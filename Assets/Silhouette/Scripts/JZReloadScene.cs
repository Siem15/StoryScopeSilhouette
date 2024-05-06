using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
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
        if (fiducialController.IsVisible) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}