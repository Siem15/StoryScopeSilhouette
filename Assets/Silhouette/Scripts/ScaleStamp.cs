using UnityEngine;

public class ScaleStamp : MonoBehaviour
{
    public float scaleFactor;
    public GameObject lastStamped;
    FiducialController _controller;
    public Vector2 scale;

    // Use this for initialization
    void Start()
    {
        _controller = gameObject.GetComponent<FiducialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastStamped != null)
        {
            Vector3 temp = lastStamped.transform.localScale;

            if (_controller.RotationSpeed > 0 && temp.y < scale.y)
            {
                lastStamped.transform.localScale += new Vector3(temp.x * _controller.RotationSpeed * scaleFactor / 10, temp.y * _controller.RotationSpeed * scaleFactor / 10, 0);
            }
            if (_controller.RotationSpeed < 0 && temp.y > scale.x)
            {
                lastStamped.transform.localScale += new Vector3(temp.x * _controller.RotationSpeed * scaleFactor / 10, temp.y * _controller.RotationSpeed * scaleFactor / 10, 0);
            }
        }
    }
}