using System.Collections.Generic;
using UnityEngine;

public class ModularSystem : MonoBehaviour
{
    // List of attach points for child objects
    [SerializeField]
    private Transform[] attachPoints;
    
    // List of all child objects
    [SerializeField]
    private List<GameObject> childObjects;

    // Sets if parent object is a child only (and thus cannot have children itself)
    [field: SerializeField]
    public bool IsChildOnly { get; private set; } = false;

    private void Start()
    {
        // Log an error if no offsets have been added
        if (attachPoints.Length <= 0) 
        {
            Debug.LogError("Modular system has no offsets; please add one or more offsets");
        }
    }
    
    public void AttachParentTo(GameObject other, Transform attachPoint) 
    {        
        transform.parent = other.transform; // Assign other object as parent
        transform.parent.position = other.transform.position + attachPoint.position; // Set new position with offset
        Debug.Log($"'{gameObject.name}' attached as a child of '{other.name}'");
    }

    public void AddChild(GameObject other)
    {
        // Exit function if parent is child only
        if (IsChildOnly)
        {
            return;
        }        

        // Check number of child objects and attach if possible
        if (childObjects.Count < attachPoints.Length)
        {
            ModularSystem modularSystem = other.GetComponent<ModularSystem>();
            modularSystem.AttachParentTo(gameObject, attachPoints[childObjects.Count + 1]);
            childObjects.Add(other);
        }
        else
        {
            Debug.LogError("ERROR: Max number of children reached");
        }
    }
}