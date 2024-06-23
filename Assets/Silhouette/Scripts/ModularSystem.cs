using System.Collections.Generic;
using UnityEngine;

public class ModularSystem : MonoBehaviour
{
    // List of attach points for child objects
    [SerializeField]
    private Transform[] attachPoints;
    
    // List of all child objects
    [field: SerializeField]
    public List<GameObject> ChildObjects { get; private set; }

    // Sets if parent object is a child only (and thus cannot have children itself)
    [field: SerializeField]
    public bool IsChildOnly { get; private set; } = false;

    private void Start()
    {
        // Log an error if no offsets have been added
        if (attachPoints.Length <= 0 && !IsChildOnly) 
        {
            Debug.LogError($"'{gameObject.name}' has no attach points; please add one or more attach points");
        }
    }
    
    public void AttachParentTo(Transform attachPoint) 
    {        
        transform.parent = attachPoint.transform; // Assign attach point as parent
        transform.position = attachPoint.position; // Set position to that of attach point
    }

    private void AddChild(GameObject other)
    {
        // Check number of child objects and attach if possible
        if (ChildObjects.Count < attachPoints.Length)
        {
            ModularSystem modularSystem = other.GetComponent<ModularSystem>();
            modularSystem.AttachParentTo(attachPoints[ChildObjects.Count]);
            ChildObjects.Add(other);
            Debug.Log($"'{gameObject.name}' attached as a child of '{other.name}'");
        }
        else
        {
            Debug.LogError("ERROR: Max number of children reached");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Exit function if parent is child only
        if (IsChildOnly)
        {
            return;
        }

        // Else, add new child object
        AddChild(collision.gameObject);
    }
}