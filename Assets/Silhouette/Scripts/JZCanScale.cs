using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZCanScale : MonoBehaviour
{
    public JZSOCameraActive jZSOCameraActive;
    FiducialController fidu;
    public float scaleFactor;
    public Transform[] otherOnes;

    void Start()
    {
        fidu = GetComponent<FiducialController>();
    }

    void Update()
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
