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
    SpriteRenderer sprite;
    private float changeTime = 0;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float timeDonker = Random.Range(2.0f, 0.5f);
        float timeLicht = Random.Range(1.0f, 0.1f);

        if (Time.time > changeTime)
        {
            sprite.enabled = !sprite.enabled;
            if (sprite.enabled) changeTime = Time.time + timeDonker;
            else changeTime = Time.time + timeLicht;
        }
    }
}