using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    [SerializeField] public bool fixedProperties;

    public int currentProperty;

    [SerializeField] public List<bool> properties = new List<bool>();

    private bool empty;

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
    private Vector3 originalRoration;
    private GameObject originalendmarker;
    private FiducialController fiducialController;
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
        properties.Add(empty);
        properties.Add(isFood);
        properties.Add(canEatFood);
        properties.Add(isFlammable);
        properties.Add(isFire);
        properties.Add(isWheel);
        properties.Add(isVehicle);

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
        originalRoration = transform.eulerAngles;
        originalendmarker = GetComponent<Character>().endMarker;
        originalWalkingSpeed = GetComponent<Character>().WalkSpeed;
        originalRunningSpeed = GetComponent<Character>().RunSpeed;

        fiducialController = GetComponent<FiducialController>();

        if (!fiducialController.AutoHideGO)
        {
            reset = true;
            connected = false;
            Debug.Log($"{gameObject.name} is not connected");
        }

        Debug.Log($"{gameObject.name} is connected");

        CheckProperty();
    }

    private void Update()
    {
        if (reset)
        {
            CheckProperty();
            reset = false;
        }

        if (connected)
        {
            if (fiducialController.m_IsVisible)
            {
                if (!isAlive)
                {
                    Debug.Log(gameObject.name + " is alive");
                    GetComponent<BoxCollider2D>().enabled = true;
                    ResetObject();
                }
                isAlive = true;
            }
            else
            {
                if (isAlive)
                {
                    Debug.Log(gameObject.name + " is not alive");
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                isAlive = false;
            }
        }

        //isAlive = !fiducialController.AutoHideGO; temp
    }

    private void FixedUpdate()
    {
        if (properties[(int)Property.IsWheel] && isAlive)
        {
            transform.Rotate(Vector3.back, 5f); // Rotate if wheel and touches a vehicle
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " trigger enter");

        Properties otherObject = collision.GetComponent<Properties>();

        Character character = GetComponent<Character>();
        Character otherCharacter = collision.GetComponent<Character>();

        // Add shadermanager to get shaders from shadermanager component
        ShaderManager shaderManager = GetComponent<ShaderManager>();

        if (otherObject != null)
        {
            if (properties[(int)Property.IsFood] && otherObject.properties[(int)Property.CanEatFood])
            {
                //TODO: spawn edible particle system
                Debug.Log($"{gameObject.name} eat");
                transform.localScale *= 0.9f; // Shrink
                otherObject.transform.localScale *= 1.1f; // Grow
            }

            if (properties[(int)Property.IsFlammable] && otherObject.properties[(int)Property.IsFire])
            {
                shaderManager.AddShader("OnFireEffectShader");  // Go to ShaderManager and use the fire shader
                Debug.Log(gameObject.name + " flame");
                if (transform.childCount > 0)
                {
                    //TODO: shaderManager.AddShader("dissolveShader")
                    //TODO: remove fire shader
                    transform.GetChild(0).gameObject.SetActive(false); // destroy
                }
            }

            if (properties[(int)Property.IsWheel] && otherObject.properties[(int)Property.IsVehicle])
            {
                Debug.Log($"{gameObject.name} attach wheel");
                transform.parent = otherObject.transform; // Stick to the vehicle
                GetComponent<BoxCollider2D>().enabled = false;
                character.endMarker = otherCharacter.endMarker;
                character.WalkSpeed = 0;
                character.RunSpeed = 0;
            }
        }
    }

    public void CheckProperty()
    {
        ResetObject();

        if (!fixedProperties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                properties[i] = false;
            }

            if (currentProperty <= properties.Count)
            {
                properties[currentProperty] = true;
            }
            else if (currentProperty > properties.Count || currentProperty < 0)
            {
                currentProperty = 0;
            }
        }
    }

    public void ResetObject()
    {
        transform.localScale = originalScale;
        transform.eulerAngles = originalRoration;
        transform.parent = null;

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = HitboxSize;
        boxCollider2D.offset = HitboxOffset;

        Character character = GetComponent<Character>();
        character.endMarker = originalendmarker;
        character.WalkSpeed = originalWalkingSpeed;
        character.RunSpeed = originalRunningSpeed;

        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}