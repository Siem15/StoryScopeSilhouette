using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamonSwitchScene : MonoBehaviour
{
    bool shuttingDown = false;
    FiducialController fidu;
    string otherScene;
    string filesLocation = "C:/StoryScope/StoryScopeMedia/Scene";
    void Start()
    {
        otherScene = gameObject.name;
        fidu = GetComponent<FiducialController>();
    }

    void Update()
    {
        if (fidu.m_IsVisible && !shuttingDown)
        {
            //LoadAnotherScene();
            StartCoroutine(NextSceneDelay());
        }
        if (!fidu.m_IsVisible && shuttingDown)
        {
            shuttingDown = false;
            StopAllCoroutines();
        }
    }

    IEnumerator NextSceneDelay()
    {
        shuttingDown = true;
        yield return new WaitForSeconds(4);
        LoadAnotherScene();
    }
    public void LoadAnotherScene()
    {
        Debug.Log(filesLocation + "/" + otherScene + ".lnk");
        System.Diagnostics.Process.Start(filesLocation + "/" + otherScene + ".lnk");
        Application.Quit();
    }
}
