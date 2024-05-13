using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZTurnSpriteCounter : MonoBehaviour
{
    public int DegreesToAction;
    float lastDegree;
    bool resetDegrees = true;
    FiducialController fiducialController;

    private void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (fiducialController.IsVisible)
        {
            if (resetDegrees)
            {
                resetDegrees = false;
                lastDegree = transform.localEulerAngles.z;
            }
            if (transform.localEulerAngles.z > lastDegree + DegreesToAction)
            {
                lastDegree = transform.localEulerAngles.z;
                UpdateSpriteCounter(-1);
            }
            if (transform.localEulerAngles.z < lastDegree - DegreesToAction)
            {
                lastDegree = transform.localEulerAngles.z;
                UpdateSpriteCounter(1);
            }
        }

        if (!fiducialController.IsVisible && !resetDegrees)
        {
            resetDegrees = true;
        }
    }

    public void UpdateSpriteCounter(int amount)
    {
        JZSpriteCounter.spriteCounter = Mathf.Clamp(JZSpriteCounter.spriteCounter += amount, 0, 100);
        //print(JZSpriteCounter.spriteCounter);
    }
}

public static class JZSpriteCounter
{
    [Range(0, 100)]
    public static int spriteCounter;
}