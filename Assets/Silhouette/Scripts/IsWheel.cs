using UnityEngine;

public class IsWheel : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.Rotate(Vector3.back, 5f); // Rotate if wheel and touches a vehicle
    }

    public void Update()
    {
        if (gameObject.GetComponent<Character>() != null)
        {
            gameObject.GetComponent<Character>().WalkSpeed = 0;
            gameObject.GetComponent<Character>().RunSpeed = 0;
        }
    }
}