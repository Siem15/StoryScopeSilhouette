using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZRotateToNextChild : MonoBehaviour
{
    GameObject[] sprites; // Stores sprites of game object.
    int currentSprite = 0; // Currently selected sprite.

    void Start()
    {
        // As game starts, create an array with size determined by number of sprites.
        sprites = new GameObject[transform.childCount];

        for (int i = 0; i < sprites.Length; i++)
        {
            // Stores different sprites in array.
            sprites[i] = transform.GetChild(i).GetChild(0).gameObject;

            // Initially, disable all sprites.
            sprites[i].SetActive(false);
        }

        // Enable initial sprite ('0') for game object.
        sprites[0].SetActive(true);
    }

    public void SelectSprite(int selectedSprite)
    {
        for (int i = 0; i < sprites.Length; i++) 
        {
            // Disable all sprites using for-loop.
            sprites[i].SetActive(false);
        }
        
        // Enable newly selected sprite based on provided value.
        sprites[Mathf.Abs(currentSprite += selectedSprite) % sprites.Length].SetActive(true);
    }
}