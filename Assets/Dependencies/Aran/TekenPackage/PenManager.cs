using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PenManager : MonoBehaviour
{
    public List<GameObject> icons;
    public GameObject pen;
    int lastSelectedIcon = 3;
    bool isactive = false;

    // Start is called before the first frame update
    void Start()
    {
        icons[lastSelectedIcon].transform.localScale = Vector3.one * 1.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pen.GetComponent<SpriteRenderer>().enabled && !isactive)
        {
            isactive = true;
            foreach (GameObject icon in icons)
            {
                icon.SetActive(true);
            }
        }
        else if (!pen.GetComponent<SpriteRenderer>().enabled && isactive)
        {
            isactive = false;
            foreach (GameObject icon in icons)
            {
                icon.SetActive(false);
            }
        }

        if (isactive)
        {
            transform.position = pen.transform.position;

            int SelectedIcon = selectedSize(pen.transform.rotation.eulerAngles.z);
            if (lastSelectedIcon != SelectedIcon)
            {
                icons[lastSelectedIcon].transform.localScale = Vector3.one;
                lastSelectedIcon = SelectedIcon;
                icons[SelectedIcon].transform.localScale = Vector3.one * 1.25f;
            }
        }
    }

    int selectedSize(float rotation)
    {
        //Debug.Log(rotation);
        if (rotation < 45) return 0;
        if (rotation < 135) return 1;
        if (rotation < 225) return 2;
        if (rotation < 315) return 3;
        return 0;
    }
}