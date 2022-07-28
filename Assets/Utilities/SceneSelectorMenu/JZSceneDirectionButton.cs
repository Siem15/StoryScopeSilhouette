using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZSceneDirectionButton : MonoBehaviour
{

    public bool speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!SceneManagerButtons.threshold)
        {
            if (speed) SceneManagerButtons.speed = 0.5f;
            if (!speed) SceneManagerButtons.speed = -0.5f;
            //print(gameObject.name + " " + speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SceneManagerButtons.speed = 0;
    }
}
