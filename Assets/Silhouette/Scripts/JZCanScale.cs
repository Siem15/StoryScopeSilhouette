using UnityEngine;

/// <summary>
/// This script is used to make sure the drawings that are scanned in can be scaled with the scaling fiducial. 
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>
public class JZCanScale : MonoBehaviour
{
    public JZSOCameraActive jZSOCameraActive;
    FiducialController fidu;
    public float scaleFactor;
    public Transform[] otherOnes;

    private void Start()
    {
        fidu = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (fidu.IsVisible && jZSOCameraActive.camIsActive)
        {
            Vector3 scale = transform.localScale;
            if (fidu.RotationSpeed > 0)
            {
                transform.localScale += new Vector3(
                    scale.x * fidu.RotationSpeed * scaleFactor / 10,
                    scale.y * fidu.RotationSpeed * scaleFactor / 10,
                    scale.z * fidu.RotationSpeed * scaleFactor / 10);
            }
            if (fidu.RotationSpeed < 0)
            {
                transform.localScale += new Vector3(
                    scale.x * fidu.RotationSpeed * scaleFactor / 10,
                    scale.y * fidu.RotationSpeed * scaleFactor / 10,
                    scale.z * fidu.RotationSpeed * scaleFactor / 10);
            }
        }
    }
}