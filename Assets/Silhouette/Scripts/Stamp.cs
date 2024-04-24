using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public bool cloned = false;
    public bool vis;
    Transform stamp;

    FiducialController fidu;

    public List<GameObject> stamps = new List<GameObject>();

    void Start()
    {
        fidu = GetComponent<FiducialController>();
        stamp = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fidu.m_IsVisible)
        {
            StopAllCoroutines();
            if (!cloned)
            {
                cloned = true;
                Vector3 temp = transform.position;
                temp.z = -4;
                Transform tempStamp = Instantiate(stamp, temp, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                stamps.Add(tempStamp.gameObject);
                tempStamp.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
                tempStamp.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (!fidu.m_IsVisible && cloned == true)
        {
            StartCoroutine(DeleteStamps());
            cloned = false;
        }
    }

    IEnumerator DeleteStamps()
    {
        yield return new WaitForSeconds(2);

        foreach (GameObject stamp in stamps)
        {
            Destroy(stamp);
        }

        stamps.Clear();
    }
}