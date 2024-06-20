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

    private GameObject EffectsManager;

    public bool reset = false;
    public bool isAlive = false;
    public bool connected = true;
    [SerializeField] private bool isAtached = false;

    public Vector2 HitboxSize;
    public Vector2 HitboxOffset;

    private Vector3 originalScale;
    private Vector3 originalRoration;
    private GameObject originalendmarker;
    private FiducialController fiducialController;
    private Character character;
    private float originalWalkingSpeed;
    private float originalRunningSpeed;
    private bool originalControlRotation;
    private bool originalFiducialRotation;
    private bool originalFiducialPos;

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
        // Add properties to a list
        if (!isAtached)
        {
            properties.Add(empty);
            properties.Add(isFood);
            properties.Add(canEatFood);
            properties.Add(isFlammable);
            properties.Add(isFire);
            properties.Add(isWheel);
            properties.Add(isVehicle);
        }

        // Add shadermanager to get shaders from shadermanager component
        EffectsManager = GameObject.Find("EffectsManager");

        // Add a BoxCollider2D component if it doesn't already exist
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Set the BoxCollider2D as a trigger
        boxCollider.isTrigger = true;

        // size of the BoxCollider2D
        boxCollider.size = HitboxSize;

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

        character = GetComponent<Character>();
        fiducialController = GetComponent<FiducialController>();

        originalScale = transform.localScale;
        originalRoration = transform.eulerAngles;

        if (character != null)
        {
            originalendmarker = character.endMarker;
            originalWalkingSpeed = character.WalkSpeed;
            originalRunningSpeed = character.RunSpeed;
            originalControlRotation = character.controlRotation;
        }


        // maks sure he is not a duplicate to preovent exidents
        if (!isAtached)
        {
            if (!fiducialController.AutoHideGO)
            {
                reset = true;
                connected = false;
                Debug.Log($"{gameObject.name} is not connected");
            }
            Debug.Log($"{gameObject.name} is connected");

            originalFiducialRotation = fiducialController.IsRotationMapped;
            originalFiducialPos = fiducialController.IsPositionMapped;

            CheckProperty();
        }
    }

    private void Update()
    {
        // resetbuton
        if (reset)
        {
            CheckProperty();
            reset = false;
        }

        //testing for effects with spase if needede
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //EffectsManager.GetComponent<EffectsManager>().AddEffect("dissolveShader", this.gameObject);
        }

        // checkes if its in a build and makes resets the object when removed and plased
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
        // Rotate if wheel and touches a vehicle
        if (properties[(int)Property.IsWheel] && isAlive)
        {
            transform.Rotate(Vector3.back, 5f);
            if (character != null)
                character.controlRotation = false;
            fiducialController.IsRotationMapped = false;
            //fiducialController.IsPositionMapped = false;
        }

        // if het is atached stop moving
        if (isAtached && character != null)
        {
            //character.endMarker = null;
            character.WalkSpeed = 0;
            character.RunSpeed = 0;
            fiducialController.IsPositionMapped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name} trigger enter");

        Properties otherObject = collision.GetComponent<Properties>();

        Character otherCharacter = collision.GetComponent<Character>();

        if (otherObject != null)
        {
            // do eating stuff
            if (properties[(int)Property.IsFood] && otherObject.properties[(int)Property.CanEatFood])
            {
                //TODO: spawn edible particle system on place of currently being eaten object
                if (EffectsManager != null)
                {
                    EffectsManager.GetComponent<EffectsManager>().AddEffect("GetsEaten", this.gameObject);
                }
                Debug.Log($"{gameObject.name} eat");
                transform.localScale *= 0.9f; // Shrink
                otherObject.transform.localScale *= 1.1f; // Grow
            }

            // do fire stuff
            if (properties[(int)Property.IsFlammable] && otherObject.properties[(int)Property.IsFire])
            {
                Debug.Log($"{gameObject.name} flame");

                if (transform.childCount >= 0)
                {
                    Invoke("hideBySize", 3f);
                    EffectsManager.GetComponent<EffectsManager>().AddEffect("dissolveShader", this.gameObject);
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                    //TODO: add shader siem
                }
            }

            // do vehicle stuff and wheel stuff
            if (properties[(int)Property.IsWheel] && otherObject.properties[(int)Property.IsVehicle])
            {

                GameObject duplicatedObject = Instantiate(this.gameObject); // make a coppy
                duplicatedObject.GetComponent<FiducialController>().IsPositionMapped = false;

                duplicatedObject.GetComponent<Properties>().originalFiducialRotation = originalFiducialRotation;
                duplicatedObject.GetComponent<Properties>().originalFiducialPos = originalFiducialPos;


                if (this.gameObject.GetComponent<JZSpriteRoot>() != null )
                    duplicatedObject.GetComponent<JZSpriteRoot>().images = this.gameObject.GetComponent<JZSpriteRoot>().images; //inport the images vrom the original

                // destroy stuff we dont need
                Destroy(this.gameObject.GetComponent<Properties>());
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                Destroy(this.gameObject.GetComponent<Rigidbody2D>());
                Destroy(this.gameObject.GetComponent<FiducialController>());
                if (character != null) 
                    Destroy(this.gameObject.GetComponent<Character>());


                this.gameObject.AddComponent<IsWheel>(); // make it spin

                if (character != null)
                {
                    character.WalkSpeed = 0;
                    character.RunSpeed = 0;
                    if (otherCharacter != null)
                    {
                        character.endMarker = otherCharacter.endMarker;
                    }
                }

                Vector3 TempPos = this.gameObject.transform.position; // save position

                this.gameObject.transform.parent = otherObject.transform; // make the wheel a child of the vehicle 

                //this.gameObject.transform.position = TempPos; // reset position

                duplicatedObject.transform.position = new Vector3(Random.Range(1000, 10000), Random.Range(1000, 10000), Random.Range(1000, 10000)); // remove the coppy from view
                duplicatedObject.GetComponent<Properties>().isAtached = true;
            }
        }
    }

    void hideBySize()
    {
        this.gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f); //hide
    }

    public void CheckProperty()
    {
        
        if (!fixedProperties)
        {
            for (int i = 0; i < properties.Count; i++) // reset the properties
            {
                properties[i] = false;
            }

            if (currentProperty <= properties.Count) // set the properties
            {
                properties[currentProperty] = true;
            }
            else if (currentProperty > properties.Count || currentProperty < 0) // if the properties controller go's out of bounse reset to 0
            {
                currentProperty = 0;
            }
        }

        ResetObject();
    }

    public void ResetObject()
    {
        transform.parent = null;
        transform.localScale = originalScale;
        transform.eulerAngles = originalRoration;
        isAtached = false;

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = HitboxSize;
        boxCollider2D.offset = HitboxOffset;
        DrawingColliderUpdate();

        if (character != null)
        {
            character.endMarker = originalendmarker;
            this.transform.position = character.endMarker.transform.position;
            character.WalkSpeed = originalWalkingSpeed;
            character.RunSpeed = originalRunningSpeed;
            character.controlRotation = originalControlRotation;
            if (character.endMarker.transform.position.x >= transform.position.x)
            {
                character.facingRight = true;
            }
        }

        fiducialController.IsRotationMapped = originalFiducialRotation;
        fiducialController.IsPositionMapped = originalFiducialPos;

        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        // add the fire effect 
        if (properties[(int)Property.IsFire])
        {
            List<GameObject> children = GetChildren();

            bool hasFire = false;
             // add only one fire
            foreach (var item in children)
            {
                if (item.name == "Fire")
                {
                    hasFire = true;
                }
            }

            if (hasFire == false)
            {
                if (EffectsManager != null)
                {
                    EffectsManager.GetComponent<EffectsManager>().AddEffect("onFireEffectShader", this.gameObject);
                }
            }


        }
        // remove all the fire if you are not a fire
        else
        {
            List<GameObject> children = GetChildren();

            foreach (var item in children)
            {
                if (item.name != "Character")
                {
                    Destroy(item);
                }
            }
        }
        // remove alle the wheels 
        if (properties[(int)Property.IsVehicle])
        {
            List<GameObject> children = GetChildren();

            foreach (var item in children)
            {
                if (item.name != "Character")
                {
                    Destroy(item);
                }
            }
        }
    }
    // make a list of all the childeren
    public List<GameObject> GetChildren()
    {
        List<GameObject> children = new List<GameObject>();
        //children.Add(gameObject);

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        return children;
    }

    //Sets the collider to the size of the drawing
    void DrawingColliderUpdate()
    {
        if (this.gameObject.GetComponent<Scan>() != null)
        {
           
            Renderer rend = GetComponent<Renderer>();
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            
            boxCollider2D.size = rend.material.GetTextureScale("_BaseMap") *1.4f;
            boxCollider2D.offset = rend.material.GetTextureOffset("_BaseMap");
        }
        else return;
    }
}