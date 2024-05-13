using UnityEngine;

/// <summary>
/// This script is used to make sure the drawings that are scanned in can be scaled with the scaling fiducial. 
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>
public class JZCanScale : MonoBehaviour
{
    public JZSOCameraActive jZSOCameraActive;
    FiducialController fiducialController;
    public float scaleFactor;
    public Transform[] otherOnes;

    private void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (fiducialController.IsVisible && jZSOCameraActive.camIsActive)
        {
            Vector3 scale = transform.localScale;

            if (fiducialController.RotationSpeed > 0)
            {
                transform.localScale += new Vector3(
                    scale.x * fiducialController.RotationSpeed * scaleFactor / 10,
                    scale.y * fiducialController.RotationSpeed * scaleFactor / 10,
                    scale.z * fiducialController.RotationSpeed * scaleFactor / 10);
            }

            if (fiducialController.RotationSpeed < 0)
            {
                transform.localScale += new Vector3(
                    scale.x * fiducialController.RotationSpeed * scaleFactor / 10,
                    scale.y * fiducialController.RotationSpeed * scaleFactor / 10,
                    scale.z * fiducialController.RotationSpeed * scaleFactor / 10);
            }
        }
    }
}