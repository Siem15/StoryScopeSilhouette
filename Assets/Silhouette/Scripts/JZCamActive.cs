using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZCamActive : MonoBehaviour
{
    FiducialController fiducialController;
    public JZSOCameraActive jZSOCameraActive;

    // Start is called before the first frame update
    private void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    private void Update()
    {
        jZSOCameraActive.camIsActive = fiducialController.m_IsVisible;
    }
}