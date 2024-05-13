using UnityEngine;

/// <summary>
/// This script is supposed to change the lighting of the scene, depending on the runtime of the game.
/// It currently does not do this yet.
/// This script does not seem to be used by any object in the game currently. 
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>

public class Flicker : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float changeTime = 0;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float timeDark = Random.Range(2.0f, 0.5f);
        float timeLight = Random.Range(1.0f, 0.1f);

        if (Time.time > changeTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            changeTime = spriteRenderer.enabled ? Time.time + timeDark : Time.time + timeLight;
        }
    }
}