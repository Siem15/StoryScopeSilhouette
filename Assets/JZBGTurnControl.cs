using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZBGTurnControl : MonoBehaviour
{
    public int DegreesToAction;
    float lastDegree;
    bool resetDegrees = true;
    FiducialController fiducialController;

    JZLoadFromExternalV2 parent;
    private void Start()
    {
        parent = transform.GetComponentInParent<JZLoadFromExternalV2>();
        fiducialController = GetComponent<FiducialController>();
    }

    /*de euler angles zijn belangrijk als je een parent hebt of niet
     als je euler gebruikt kan je een parent hebben*/
    private void Update()
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
                parent.ShowNextItem(true);
            }
            if (transform.eulerAngles.z < lastDegree - DegreesToAction)
            {
                lastDegree = transform.eulerAngles.z;
                parent.ShowNextItem(false);
            }
        }

        if (!fiducialController.IsVisible && !resetDegrees)
        {
            resetDegrees = true;
        }
    }
}