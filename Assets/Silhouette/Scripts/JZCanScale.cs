using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZCanScale : MonoBehaviour
{
    public JZSOCameraActive jZSOCameraActive;
    FiducialController fiducialController;
    public float scaleFactor;

    void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    void Update()
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
