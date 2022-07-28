using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZButtonSequence : MonoBehaviour
{
    public GameObject activateGO, levelSelector, volume;
    FiducialController fidu;
    bool x;
    private void Start()
    {
        fidu = GetComponent<FiducialController>();
        Invoke("Delay", 3);
    }
    void Update()
    {
        if (!x) return;
        LevelSelect();
    }

    public void Delay(){x = true;}

    private void LevelSelect()
    {
        if (Vector3.Distance(transform.position, activateGO.transform.position) < 2)
        {
            levelSelector.SetActive(true);
            activateGO.SetActive(false);
        }
        if (!fidu.IsVisible)
        {
            levelSelector.SetActive(false);
            activateGO.SetActive(false);
            volume.SetActive(false);
        }
        else
        {
            volume.SetActive(true);
        }
        if (fidu.IsVisible && !levelSelector.activeSelf) activateGO.SetActive(true);
        else activateGO.SetActive(false);
    }
}
