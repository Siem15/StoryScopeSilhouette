using UnityEngine;

public class RotateProperties : MonoBehaviour
{
    public int propertyIndex = 0;
    [SerializeField] int propertiesAmount = 10;
    Properties[] propertyObjects;
    [SerializeField]  Properties clostestObject;


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
