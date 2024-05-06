using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RamonSwitchScene : MonoBehaviour
{
    bool shuttingDown = false;
    FiducialController fidu;
    string otherScene;
    readonly string filesLocation = "C:/StoryScope/StoryScopeMedia/Scene";

    private void Start()
    {
        otherScene = gameObject.name;
        fidu = GetComponent<FiducialController>();
    }

    private void Update()
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

    private IEnumerator NextSceneDelay()
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