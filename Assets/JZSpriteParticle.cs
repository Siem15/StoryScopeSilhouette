using UnityEngine;

public class JZSpriteParticle : MonoBehaviour
{
    Material sprRend;
    public Texture2D[] multipleLoaded;

    private void Start()
    {
        sprRend = GetComponent<ParticleSystemRenderer>().material;
    }

    private void Update()
    {
        if (multipleLoaded.Length == 0) return;
        sprRend.mainTexture = multipleLoaded[JZSpriteCounter.spriteCounter % multipleLoaded.Length];
    }
}