using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZSpriteChild : MonoBehaviour
{
    Sprite ogSprite;
    Vector2 spritePivot;
    SpriteRenderer sprRend;
    public List<Texture2D> images = new List<Texture2D>();
    public Sprite [] multipleLoaded;
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        ogSprite = sprRend.sprite;

        GetImagesFromRoot();

        spritePivot = CalculatePivot();
        multipleLoaded = new Sprite[images.Count];
        SpriteSlicer();
    }

    private void GetImagesFromRoot()
    {
        images = transform.root.GetComponent<JZSpriteRoot>().images;
    }

    public Vector2 CalculatePivot()
    {
        Bounds bounds = sprRend.sprite.bounds;
        return new Vector2(-bounds.center.x / bounds.extents.x / 2 + 0.5f,
                          -bounds.center.y / bounds.extents.y / 2 + 0.5f);
    }
    public void SpriteSlicer()
    {
        for (int i = 0; i < multipleLoaded.Length; i++)
        {
            multipleLoaded[i] = Sprite.Create(images[i], sprRend.sprite.rect, spritePivot, 100, 0, SpriteMeshType.FullRect); //slice the sprite 
        }
    }

    private void Update()
    {
        if (multipleLoaded.Length== 0) return;
        sprRend.sprite = multipleLoaded[JZSpriteCounter.spriteCounter % multipleLoaded.Length];

    }
}
