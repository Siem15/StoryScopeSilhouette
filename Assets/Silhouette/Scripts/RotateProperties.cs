using UnityEngine;

public class RotateProperties : MonoBehaviour
{
    [SerializeField] int propertiesAmount = 10;
    Properties[] propertyObjects;
    [SerializeField] Properties clostestObject;
    [SerializeField] GameObject icon;
    private Camera cam;
    private FiducialController FC;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        FC = GetComponent<FiducialController>();
        icon.SetActive(false);
    }



    private void FixedUpdate()
    {
        propertyObjects = FindObjectsOfType<Properties>();
        float shortestDistance = 100f;

        foreach (Properties property in propertyObjects)
        {
            float currentDistance = Vector3.Distance(this.transform.position, property.transform.position);
            if (currentDistance < shortestDistance && property.isAlive)
            {
                shortestDistance = currentDistance;
                clostestObject = property;
            }
        }
        ManageUI();
    }

    //This function should manage how the icons for selected object and properties work
    void ManageUI()
    {
        if (clostestObject != null && (FC.IsVisible || !FC.AutoHideGO))
        {
            icon.SetActive(true);
            icon.transform.position = cam.WorldToScreenPoint(clostestObject.transform.position);
            //TODO: change image of icon based on selected property
        }
        else
        {
            icon.SetActive(false);
        }
    }


    //NOTE: this function should be called with the OnRotateForward and OnRotateBackward with te corresponding direction
    public void NextProperty(int direction)
    {
        clostestObject.currentPropertie += direction;
        

        //Resets to top when below 0
        if (clostestObject.currentPropertie < 0)
            clostestObject.currentPropertie = propertiesAmount;
        //Resets to 0 when higher than amount of properties
        if (clostestObject.currentPropertie > propertiesAmount)
            clostestObject.currentPropertie = 0;

        clostestObject.ResetObject();
    }
}
