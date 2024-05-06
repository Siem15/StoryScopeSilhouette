using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZVariables : MonoBehaviour
{
    FiducialController fidu;

    public float angle, angleDegrees;

    // Start is called before the first frame update
    private void Start()
    {
        fidu = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    private void Update()
    {
        angle = fidu.Angle;
        angleDegrees = fidu.AngleDegrees;
    }
}