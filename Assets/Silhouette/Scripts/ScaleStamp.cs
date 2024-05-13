using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ScaleStamp : MonoBehaviour
{
    public float scaleFactor;
    public GameObject lastStamped;
    FiducialController _controller;
    public Vector2 scale;

    // Use this for initialization
    private void Start()
    {
        _controller = gameObject.GetComponent<FiducialController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastStamped == null)
        {
            return;
        }

        Vector3 temp = lastStamped.transform.localScale;

        if (_controller.RotationSpeed > 0 && temp.y < scale.y)
        {
            Vector3 addToLocalScale = new Vector3(temp.x * _controller.RotationSpeed * scaleFactor / 10, 
                temp.y * _controller.RotationSpeed * scaleFactor / 10, 0);
            lastStamped.transform.localScale += addToLocalScale;
        }
        if (_controller.RotationSpeed < 0 && temp.y > scale.x)
        {
            Vector3 addToLocalScale = new Vector3(temp.x * _controller.RotationSpeed * scaleFactor / 10, 
                temp.y * _controller.RotationSpeed * scaleFactor / 10, 0);
            lastStamped.transform.localScale += addToLocalScale;
        }
    }
}