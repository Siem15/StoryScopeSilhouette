using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZVariables : MonoBehaviour
{
    FiducialController fiducialController;
    public float angle, angleDegrees;

    // Start is called before the first frame update
    private void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    private void Update()
    {
        angle = fiducialController.Angle;
        angleDegrees = fiducialController.AngleDegrees;
    }
}