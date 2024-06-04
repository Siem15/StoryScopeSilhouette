using UnityEngine;

/// <summary>
/// 
/// </summary>
public class StoryScopePen : MonoBehaviour
{
    FreeDraw.Drawable drawable = null;
    public Color PenColor;
    public int PenWidth;

    // Start is called before the first frame update
    private void Start()
    {
        drawable = GameObject.Find("TekenVlak").GetComponent<FreeDraw.Drawable>();
        drawable.PenSpriteRef = (GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    private void Update()
    {
        if (GetComponent<SpriteRenderer>().enabled)
        {
            drawable.PenPos = transform.position;
        }

        float PenScale = PenSize(transform.rotation.eulerAngles.z);
        PenWidth = (int)PenScale;

        transform.localScale = new Vector3(PenScale / 5, PenScale / 5, PenScale / 5);
    }

    private float PenSize(float rotation)
    {
        //Debug.Log(rotation);
        if (rotation < 45) return 3;
        if (rotation < 135) return 6;
        if (rotation < 225) return 9;
        if (rotation < 315) return 12;
        return 3;
    }
}