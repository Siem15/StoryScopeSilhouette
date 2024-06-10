using UnityEngine;

public class ModularSystem : MonoBehaviour
{
    private int wheelsAttached;

    private Vector3 firstWheelPosition, secondWheelPosition;

    [SerializeField]
    private Vector3 firstWheelOffset;

    [SerializeField]
    private Vector3 secondWheelOffset;

    // Start is called before the first frame update
    void Start()
    {
        wheelsAttached = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Properties otherObject = collision.GetComponent<Properties>();

        firstWheelPosition = otherObject.transform.position + firstWheelOffset;
        secondWheelPosition = otherObject.transform.position + secondWheelOffset;
    }
}