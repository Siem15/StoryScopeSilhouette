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
    public bool isAlive = false;
    public bool connected = true;

    private Vector3 originalScale;
    private GameObject originalendmarker;
    private FiducialController FC;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;

    void Start()
    {
        originalScale = transform.localScale;
        originalendmarker = this.GetComponent<Character>().endMarker;
        originalWalkingSpeed = this.GetComponent<Character>().WalkSpeed;
        originalRunningSpeed = this.GetComponent<Character>().RunSpeed;
        FC = this.GetComponent<FiducialController>();
        if (!FC.AutoHideGO)
        {
            reset = true;
            connected = false;
        }
    }

    private void Update()
    {
        if (reset)
        {
            ResetObject();
            reset = false;
        }

        if (connected)
        {

            if (FC.m_IsVisible)
            {
                if (!isAlive)
                {
                    ResetObject();
                }
                isAlive = true;
            }
            else
            {
                if (isAlive)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                }
                isAlive = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isWheel)
        {
            transform.Rotate(Vector3.back, 5f); // Rotate if wheel and touches a vehicle
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
                if (transform.childCount > 0)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
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
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}