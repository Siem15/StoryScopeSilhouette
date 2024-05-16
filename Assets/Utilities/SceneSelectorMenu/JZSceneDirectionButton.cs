using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSceneDirectionButton : MonoBehaviour
{
    public bool speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!SceneManagerButtons.Threshold)
        {
            SceneManagerButtons.Speed = speed ? 0.5f : -0.5f;

            // NOTE: Uncomment these if-statements if above statement does not work
            //if (speed) SceneManagerButtons.speed = 0.5f;
            //if (!speed) SceneManagerButtons.speed = -0.5f;
            //print(gameObject.name + " " + speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => SceneManagerButtons.Speed = 0;
}