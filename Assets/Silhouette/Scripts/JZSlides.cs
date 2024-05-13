using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSlides : MonoBehaviour
{
    int nextSlide = 0;
    public Sprite[] slides;
    SpriteRenderer spriteRenderer;
    FiducialController fiducialController;

    public int DegreesToAction;
    float lastDegree;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fiducialController = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (!fiducialController.IsVisible)
        {
            return;
        }

        if (fiducialController.AngleDegrees > lastDegree + DegreesToAction) Slide(false); //stuff turn left back
        if (fiducialController.AngleDegrees < lastDegree - DegreesToAction) Slide(true);  //stuff turn right forward
    }

    public void Slide(bool next)
    {
        lastDegree = fiducialController.AngleDegrees;
        nextSlide += next ? 1 : -1;

        if (nextSlide < 0) 
        {
            nextSlide = slides.Length - 1;
        }
        
        spriteRenderer.sprite = slides[nextSlide %= slides.Length];
    }
}