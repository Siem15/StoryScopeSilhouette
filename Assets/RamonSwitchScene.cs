using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RamonSwitchScene : MonoBehaviour
{
    FiducialController fiducialController; // Used to check rotation of game object.
    bool shuttingDown = false; // Stores if current scene is being shut down or not.
    string nextScene; // Stores name of next scene to be loaded
    readonly string sceneFolderPath = "C:/StoryScope/StoryScopeMedia/Scene"; // Location where scene logs are stored.

    private void Start()
    {
        nextScene = gameObject.name; // Scene name is derived from game object.
        fiducialController = GetComponent<FiducialController>();
    }

    private void Update()
    {
        // Load next scene if fiducial is visible and scene is not being shut down.
        if (fiducialController.m_IsVisible && !shuttingDown)
        {
            //LoadAnotherScene();
            StartCoroutine(NextSceneDelay());
        }

        // Once scene has been shut down and fiducial is invisible, stop coroutines and reset values.
        if (!fiducialController.m_IsVisible && shuttingDown)
        {
            shuttingDown = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator NextSceneDelay()
    {
        // Set current scene to be shut down, wait a few seconds and then load next scene.
        shuttingDown = true;
        yield return new WaitForSeconds(4.0f);
        LoadAnotherScene();
    }

    public void LoadAnotherScene()
    {
        // Print out location of newly loaded scene, then quit application.
        string nextSceneFilePath = $"{sceneFolderPath}/{nextScene}.lnk";
        Debug.Log(nextSceneFilePath);
        System.Diagnostics.Process.Start(nextSceneFilePath);
        Application.Quit();
    }
}