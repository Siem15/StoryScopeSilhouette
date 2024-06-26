using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : MonoBehaviour
{
    [SerializeField] float spinDuration = 1f;
    FiducialController FC;

    private void Start()
    {
        FC = GetComponent<FiducialController>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && (FC.IsVisible|| !FC.AutoHideGO))
        {
            StartCoroutine(Rotate(collision.gameObject, spinDuration));
        }
    }

    //Rotates object one time
    IEnumerator Rotate(GameObject rObject, float duration)
    {
        float startRotation = rObject.transform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            rObject.transform.eulerAngles = new Vector3(rObject.transform.eulerAngles.x, rObject.transform.eulerAngles.y, zRotation);
            yield return null;
        }
        //Resets rotation in case spin doesn't finish properly
        rObject.transform.eulerAngles = new Vector3(rObject.transform.eulerAngles.x, rObject.transform.eulerAngles.y, 0);
    }
}
