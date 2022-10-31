using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZButtonSequence : MonoBehaviour
{
    public GameObject activateGO, levelSelector, volume;
    FiducialController fidu;
    public Transform fill;
    public SpriteRenderer sprRend;
    public Vector2 emptyPos, fillPos;
    bool x;
    private void Start()
    {
        fidu = GetComponent<FiducialController>();
        Invoke("Delay", 3);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(LerpPosition(fillPos, 3));
            sprRend.color = Random.ColorHSV(0f, 1f, .5f, .5f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(LerpPosition(emptyPos, 0.5f));

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("SelectionsImage"))
        {
            StopAllCoroutines();
            StartCoroutine(LerpPosition(fillPos, 3));
            //sprRend.color = Random.ColorHSV(0f, 1f, .5f, .5f, 1f, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(LerpPosition(emptyPos, 0.5f));
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = fill.localPosition;
        while (time < duration)
        {
            fill.localPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        fill.localPosition = targetPosition;
    }
}
