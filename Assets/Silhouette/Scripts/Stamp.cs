using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {


    bool cloned = false;
    Transform stamp;

    FiducialController fidu;

    public List<GameObject> stamps = new List<GameObject>();

	void Start () {
        fidu = GetComponent<FiducialController>();
        stamp = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update () {

        if (fidu.m_IsVisible || Input.GetKeyDown(KeyCode.O))
        {
            StopAllCoroutines();
            if (!cloned)
            {
                cloned = true;
                Vector3 temp = transform.position;
                temp.z = -4;
                Transform  tempStamp =Instantiate(stamp, temp, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                stamps.Add(tempStamp.gameObject);
                tempStamp.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
                tempStamp.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (!fidu.m_IsVisible)
        {
            StartCoroutine(deleteStamps());
            cloned = false;
        }
    }

    IEnumerator deleteStamps()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject stamp in stamps)
        {
            Destroy(stamp);
        }
        stamps.Clear();
    }
}
