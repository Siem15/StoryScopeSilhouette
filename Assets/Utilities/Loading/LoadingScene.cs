using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    private void Start() => LoadScene(1);

    public void LoadScene(int sceneID) => StartCoroutine(LoadSceneAsync(sceneID));

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!asyncOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}