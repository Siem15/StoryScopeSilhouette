using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    SpriteRenderer sprite;
    private float  changeTime = 0;

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
