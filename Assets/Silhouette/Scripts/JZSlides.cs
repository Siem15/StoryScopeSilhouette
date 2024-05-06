using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSlides : MonoBehaviour
{
    int nextSlide = 0;
    public Sprite[] slides;
    SpriteRenderer spr;
    FiducialController fiducialController;

    public int DegreesToAction;
    float lastDegree;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        fiducialController = GetComponent<FiducialController>();
    }

    private void Update()
    {
        if (fiducialController.IsVisible)
        {
            if (fiducialController.AngleDegrees > lastDegree + DegreesToAction) Slide(false); //stuff turn left back
            if (fiducialController.AngleDegrees < lastDegree - DegreesToAction) Slide(true);  //stuff turn right forward
        }
    }

    public void Slide(bool next)
    {
        lastDegree = fiducialController.AngleDegrees;
        nextSlide += next ? 1 : -1;
        if (nextSlide < 0) nextSlide = slides.Length - 1;
        spr.sprite = slides[nextSlide %= slides.Length];
    }
}