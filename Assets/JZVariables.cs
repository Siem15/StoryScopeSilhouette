using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZVariables : MonoBehaviour
{
    FiducialController fidu;

    public float angle, angleDegrees;


    // Start is called before the first frame update
    void Start()
    {
        fidu = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    void Update()
    {
        angle = fidu.Angle;
        angleDegrees = fidu.AngleDegrees;
    }
}
