using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public int currentPropertie;

    [SerializeField] public List<bool> properties = new List<bool>();

    private bool enpty;
    [SerializeField] private bool isFood = false;
    [SerializeField] private bool canEatFood = false;
    [SerializeField] private bool isFlammable = false;
    [SerializeField] private bool isFire = false;
    [SerializeField] private bool isWheel = false;
    [SerializeField] private bool isVehicle = false;

    public bool reset = false;
    public bool isAlive = false;
    public bool connected = true;

    public Vector2 HitboxSize;
    public Vector2 HitboxOffset;

    private Vector3 originalScale;
    private GameObject originalendmarker;
    private FiducialController FC;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;

    public enum Property
    {
        Empty,
        IsFood,
        CanEatFood,
        IsFlammable,
        IsFire,
        IsWheel,
        IsVehicle,
    }

    void Start()
    {
        properties.Add(enpty);
        properties.Add(isFood);
        properties.Add(canEatFood);
        properties.Add(isFlammable);
        properties.Add(isFire);
        properties.Add(isWheel);
        properties.Add(isVehicle);

        checkPropertie();

        // Add a BoxCollider2D component if it doesn't already exist
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Set the BoxCollider2D as a trigger
        boxCollider.isTrigger = true;

        // size of the BoxCollider2D
        boxCollider.size *= HitboxSize;

        // size of the BoxCollider2D
        boxCollider.offset = HitboxOffset;

        // turn off the BoxCollider2D
        boxCollider.enabled = false;

        // Add a Rigidbody2D component if it doesn't already exist
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D == null)
        {
            rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        }

        // Set the gravity scale of the Rigidbody2D to 0
        rigidbody2D.gravityScale = 0;
        rigidbody2D.mass = 0.0001f;
        rigidbody2D.angularDrag = 0;

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

        isAlive = !FC.AutoHideGO; // temp
    }

    private void FixedUpdate()
    {
        if (properties[(int)Property.IsWheel])
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
            // Vriendelijke vriend, 2 is ISFood
            if (properties[(int)Property.IsFood] && otherObject.properties[(int)Property.CanEatFood])
            {
                Debug.Log("eat");
                transform.localScale *= 0.9f; // Shrink
                otherObject.transform.localScale *= 1.1f; // Grow
            }
            if (properties[(int)Property.IsFlammable] && otherObject.properties[(int)Property.IsFire])
            {
                Debug.Log("flame");
                if (transform.childCount > 0)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            if (properties[(int)Property.IsWheel] && otherObject.properties[(int)Property.IsVehicle])
            {
                Debug.Log("attach wheel");
                transform.parent = otherObject.transform; // Stick to the vehicle
                this.GetComponent<BoxCollider2D>().enabled = false;
                thisCharacter.endMarker = otherCharacter.endMarker;
                thisCharacter.WalkSpeed = 0;
                thisCharacter.RunSpeed = 0;
            }
        }
    }

    public void checkPropertie()
    {
        if (currentPropertie != 0)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                properties[i] = false;
            }

            if (currentPropertie <= properties.Count || currentPropertie < 0)
            {
                properties[currentPropertie] = true;
            }
            else
                currentPropertie = 0;
        }
    }

    public void ResetObject()
    {
        checkPropertie();
        transform.localScale = originalScale;
        transform.parent = null;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<BoxCollider2D>().size = HitboxSize;
        this.GetComponent<BoxCollider2D>().offset = HitboxOffset;
        this.GetComponent<Character>().endMarker = originalendmarker;
        this.GetComponent<Character>().WalkSpeed = originalWalkingSpeed;
        this.GetComponent<Character>().RunSpeed = originalRunningSpeed;
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
