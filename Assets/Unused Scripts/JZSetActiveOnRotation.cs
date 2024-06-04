using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSetActiveOnRotation : MonoBehaviour
{
    FiducialController fiducialController; // Used to check rotation of game object.
    GameObject[] sprites; // Stores various sprites of game object.
    int spriteIndex; // Maintains index of currently active sprite.

    private void Start()
    {
        // Get component for fiducial controller.
        fiducialController = GetComponent<FiducialController>(); 

        // Initialize array of sprites.
        sprites = new GameObject[4];

        for (int i = 0; i < sprites.Length; i++)
        {
            // Get sprites from game object.
            sprites[i] = transform.GetChild(i).GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        // Check game object rotation if fiducial controller is visible.
        if (fiducialController.IsVisible) 
        {
            CheckRotation(fiducialController.AngleDegrees);
        }        
    }

    void CheckRotation(float angle)
    {
        // Return if sprite index is less than zero.
        if (spriteIndex < 0)
        {
            return;
        }

        // Set sprite index based on angle of game object.
        if (angle > 315 || angle < 45) spriteIndex = 0;
        if (angle > 46 && angle < 135) spriteIndex = 1;
        if (angle > 136 && angle < 225) spriteIndex = 2;
        if (angle > 226 && angle < 314) spriteIndex = 3;

        // Set sprite being displayed based on sprite index.
        SelectSprite(spriteIndex);
    }

    public void SelectSprite(int spriteIndex)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            // Disable all sprites.
            sprites[i].SetActive(false);
        }

        // Enable only the selected sprite.
        sprites[spriteIndex].SetActive(true);
    }
}