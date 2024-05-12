using UnityEngine;

public class Properties : MonoBehaviour
{
    public bool isFood = false;
    public bool canEatFood = false;
    public bool isFlammable = false;
    public bool isFire = false;
    public bool isWheel = false;
    public bool isVehicle = false;
    public bool reset = false;

    private Vector3 originalScale;
    private GameObject originalendmarker;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;

    void Start()
    {
        originalScale = transform.localScale;
        originalendmarker = this.GetComponent<Character>().endMarker;
        originalWalkingSpeed = this.GetComponent<Character>().WalkSpeed;
        originalRunningSpeed = this.GetComponent<Character>().RunSpeed;
    }

    private void FixedUpdate()
    {
        if (isWheel)
        {
            transform.Rotate(Vector3.back, 5f); // Rotate if wheel and touches a vehicle
        }

        if (reset)
        {
            ResetObject();
            reset = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
        Properties otherObject = collision.GetComponent<Properties>();
        Character thisCharacter = this.GetComponent<Character>();
        Character otherCharacter = collision.GetComponent<Character>();
        if (otherObject != null)
        {
            if (isFood && otherObject.canEatFood)
            {
                Debug.Log("eat");
                transform.localScale *= 0.9f; // Shrink
                otherObject.transform.localScale *= 1.1f; // Grow
            }
            if (isFlammable && otherObject.isFire)
            {
                Debug.Log("flame");
                Destroy(gameObject); // Destroy if flammable and touches fire
            }
            if (isWheel && otherObject.isVehicle)
            {
                Debug.Log("atatch wheel");
                transform.parent = otherObject.transform; // Stick to the vehicle
                this.GetComponent<BoxCollider2D>().enabled = false;
                thisCharacter.endMarker = otherCharacter.endMarker;
                thisCharacter.WalkSpeed = 0;
                thisCharacter.RunSpeed = 0;
            }
        }
    }

    public void ResetObject()
    {
        transform.localScale = originalScale;
        transform.parent = null;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<Character>().endMarker = originalendmarker;
        this.GetComponent<Character>().WalkSpeed = originalWalkingSpeed;
        this.GetComponent<Character>().RunSpeed = originalRunningSpeed;
    }
}