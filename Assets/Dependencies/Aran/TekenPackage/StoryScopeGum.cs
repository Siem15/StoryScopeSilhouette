using UnityEngine;

/// <summary>
/// 
/// </summary>
public class StoryScopeGum : MonoBehaviour
{
    FreeDraw.Drawable drawable = null;
    public Color PenColor;
    public int PenWidth;

    // Start is called before the first frame update
    void Start()
    {
        drawable = GameObject.Find("TekenVlak").GetComponent<FreeDraw.Drawable>();
        drawable.GumSpriteRef = (GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().enabled) drawable.PenPos = transform.position;
    }
}