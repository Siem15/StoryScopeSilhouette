using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZBGTurnControl : MonoBehaviour
{
    public int DegreesToAction;
    float lastDegree;
    bool resetDegrees = true;
    FiducialController fiducialController;

    JZLoadFromExternalV2 parent;
    void Start()
    {
        parent = transform.GetComponentInParent<JZLoadFromExternalV2>();
        fiducialController = GetComponent<FiducialController>();
    }

    /*de euler angles zijn belangrijk als je een parent hebt of niet
     als je euler gebruikt kan je een parent hebben*/

    void Update()
    {
        GetRotation();
    }

    private void GetRotation()
    {
        if (fiducialController.IsVisible)
        {
            if (resetDegrees)
            {
                resetDegrees = false;
                lastDegree = transform.eulerAngles.z;
            }
            if (transform.eulerAngles.z > lastDegree + DegreesToAction)
            {
                lastDegree = transform.eulerAngles.z;
                parent.Next(true);
            }
            if (transform.eulerAngles.z < lastDegree - DegreesToAction)
            {
                lastDegree = transform.eulerAngles.z;
                parent.Next(false);
            }
        }
        if (!fiducialController.IsVisible && !resetDegrees)
        {
            resetDegrees = true;
        }
    }
}
