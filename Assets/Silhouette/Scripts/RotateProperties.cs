using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProperties : MonoBehaviour
{
    public int propertyIndex = 0;
    [SerializeField] int propertiesAmount = 10;

    //NOTE: this function should be called with the OnRotateForward and OnRotateBackward with te corresponding direction
    public void NextProperty(int direction)
    {
        propertyIndex += direction;

        //Resets to top when below 0
        if (propertyIndex < 0)
        propertyIndex = propertiesAmount;
        //Resets to 0 when higher than amount of properties
        if (propertyIndex > propertiesAmount)
            propertyIndex = 0;
        
    }


}
