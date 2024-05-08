using UnityEngine;

/// <summary>
/// This script uses the boolean from JZSOCameraActive to turn on the camera that is used by the fiducialcontroller,
/// So that the camera is able to be seen by the user.
/// I would like to see these camera related scripts such as JZSOCameraActive, JZCamActive, JZCanScale
/// be compacted into 1 script, to reduce the redundancy of the code. 
/// 
/// - Siem Wesseling, 08/05/2024
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