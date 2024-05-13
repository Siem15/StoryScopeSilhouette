using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSpriteChild : MonoBehaviour
{
    Vector2 spritePivot;
    SpriteRenderer spriteRenderer;
    float originalSpritePixelsPerUnit;
    public List<Texture2D> images = new List<Texture2D>();
    Sprite[] arrayOfImages;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpritePixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        images = transform.root.GetComponent<JZSpriteRoot>().images;
        spritePivot = CalculatePivot();
        arrayOfImages = new Sprite[images.Count];
        SpriteSlicer();
    }

    public Vector2 CalculatePivot()
    {
        Bounds bounds = spriteRenderer.sprite.bounds;
        float pivotX = -bounds.center.x / bounds.extents.x / 2 + 0.5f;
        float pivotY = -bounds.center.y / bounds.extents.y / 2 + 0.5f;
        return new Vector2(pivotX, pivotY);
    }

    public void SpriteSlicer()
    {
        for (int i = 0; i < arrayOfImages.Length; i++)
        {
            arrayOfImages[i] = Sprite.Create(images[i], spriteRenderer.sprite.rect, spritePivot,
                originalSpritePixelsPerUnit, 0, SpriteMeshType.FullRect); // Slice the sprite 
        }
    }

    private void Update()
    {
        if (arrayOfImages.Length == 0)
        {
            return;
        }

        spriteRenderer.sprite = arrayOfImages[JZSpriteCounter.spriteCounter % arrayOfImages.Length];
    }
}