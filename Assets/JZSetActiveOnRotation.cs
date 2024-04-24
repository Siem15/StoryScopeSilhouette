using UnityEngine;

public class JZSetActiveOnRotation : MonoBehaviour
{
    FiducialController fidu;
    GameObject[] children = new GameObject[4];
    int spriteCheck = -1;
    int sprite;
    void Start()
    {
        fidu = GetComponent<FiducialController>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).GetChild(0).gameObject;
        }
    }

    void Update()
    {
        if (fidu.IsVisible) CheckRotation(fidu.AngleDegrees);
    }
    void CheckRotation(float angle)
    {
        if (angle > 315 || angle < 45) sprite = 0;
        if (angle > 46 && angle < 135) sprite = 1;
        if (angle > 136 && angle < 225) sprite = 2;
        if (angle > 226 && angle < 314) sprite = 3;


        if (spriteCheck != sprite) ActivateSprite(sprite);
    }

    public void ActivateSprite(int spr)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActive(false);
        }
        children[spr].SetActive(true);
        spriteCheck = spr;
    }
}
