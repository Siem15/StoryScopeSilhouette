using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateProperties : MonoBehaviour
{
    [SerializeField] int propertiesAmount = 10;
    Properties[] propertyObjects;
    [SerializeField] Properties clostestObject;
    [SerializeField] Image icon;
    [SerializeField] List<Sprite> icons = new List<Sprite>();
    private Camera cam;
    private FiducialController FC;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        FC = GetComponent<FiducialController>();
        icon.enabled = false;
    }



    private void FixedUpdate()
    {
        propertyObjects = FindObjectsOfType<Properties>();
        float shortestDistance = 100f;

        //Looks for the closest object that has properties and selects it 
        foreach (Properties property in propertyObjects)
        {
            float currentDistance = Vector3.Distance(this.transform.position, property.transform.position);
            if (currentDistance < shortestDistance && property.isAlive && !property.fixedProperties)
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
            icon.enabled = true;
            icon.transform.position = cam.WorldToScreenPoint(clostestObject.transform.position);
            //TODO: change image of icon based on selected property
            if(clostestObject.currentProperty < icons.Count)
            {
                icon.sprite = icons[clostestObject.currentProperty];
            }
            else
            {
                icon.sprite = icons[0];
            }
        }
        else
        {
            icon.enabled = false;
        }
    }


    //NOTE: this function should be called with the OnRotateForward and OnRotateBackward with te corresponding direction
    public void NextProperty(int direction)
    {
        if (clostestObject != null && !clostestObject.fixedProperties)
        {
            clostestObject.currentProperty += direction;


            //Resets to top when below 0
            if (clostestObject.currentProperty < 0)
                clostestObject.currentProperty = propertiesAmount;
            //Resets to 0 when higher than amount of properties
            if (clostestObject.currentProperty > propertiesAmount)
                clostestObject.currentProperty = 0;

            clostestObject.CheckProperty();
        }
    }
}
