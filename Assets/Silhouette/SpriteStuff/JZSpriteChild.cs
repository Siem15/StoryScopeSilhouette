using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZSpriteChild : MonoBehaviour
{
    Vector2 spritePivot;
    SpriteRenderer sprRend;
    float originalSpritePixelsPerUnit;
    public List<Texture2D> images = new List<Texture2D>();
    Sprite [] multipleLoaded;
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        originalSpritePixelsPerUnit = sprRend.sprite.pixelsPerUnit;
        images = transform.root.GetComponent<JZSpriteRoot>().images;
        spritePivot = CalculatePivot();
        multipleLoaded = new Sprite[images.Count];
        SpriteSlicer();

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
            multipleLoaded[i] = Sprite.Create(images[i], sprRend.sprite.rect, spritePivot, originalSpritePixelsPerUnit, 0, SpriteMeshType.FullRect); //slice the sprite 
        }
    }

    private void Update()
    {
        if (multipleLoaded.Length == 0) return;
        sprRend.sprite = multipleLoaded[JZSpriteCounter.spriteCounter % multipleLoaded.Length];
    }
}
