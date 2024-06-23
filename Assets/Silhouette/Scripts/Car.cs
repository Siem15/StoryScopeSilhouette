using UnityEngine;

public class Car : MonoBehaviour
{
    // Modular system attached to car
    private ModularSystem modularSystem;

    private void Start()
    {
        modularSystem = gameObject.GetComponent<ModularSystem>();
    }

    private void Update()
    {
        // Attach 'IsWheel'-component to child if not present
        foreach (GameObject child in modularSystem.ChildObjects)
        {
            if (child.GetComponent<IsWheel>() == null) 
            {
                child.AddComponent<IsWheel>();
            }
        }
    }
}