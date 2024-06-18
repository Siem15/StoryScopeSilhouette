using System.Collections;
using UnityEngine;

public class BananaPeel : MonoBehaviour
{
    [SerializeField] 
    float spinDuration = 1f;

    FiducialController fiducialContoller;

    private void Start()
    {
        fiducialContoller = GetComponent<FiducialController>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && (fiducialContoller.IsVisible || !fiducialContoller.AutoHideGO))
        {
            StartCoroutine(Rotate(collision.gameObject, spinDuration));
        }
    }

    IEnumerator Rotate(GameObject gameObject, float duration)
    {
        float startRotation = gameObject.transform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, time / duration) % 360.0f;
            gameObject.transform.eulerAngles = 
                new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, zRotation);
            yield return null;
        }

        gameObject.transform.eulerAngles = 
            new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 0);
    }
}