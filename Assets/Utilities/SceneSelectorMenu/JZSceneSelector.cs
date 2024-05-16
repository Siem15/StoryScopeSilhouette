using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// </summary>
public class JZSceneSelector : MonoBehaviour
{
    public string pathToImage;
    public float speed;
    public float startPosition, endPosition;
    public Texture2D icon;
    string scenePath;

    private void Start()
    {
#if UNITY_STANDALONE_WIN
        scenePath = pathToImage + "/StoryScope.exe";
#endif
#if UNITY_STANDALONE_LINUX
        pathToScene = pathToImage + "/StoryScope.x86_64";
#endif

        StartCoroutine(LoadAll(Directory.GetFiles(pathToImage + "/icon/")));
    }

    private void Update()
    {
        if (SceneManagerButtons.Threshold)
        {
            if (transform.position.x > endPosition) transform.position = new Vector3(endPosition, transform.position.y, 0);
            if (transform.position.x < startPosition) transform.position = new Vector3(startPosition, transform.position.y, 0);
        }
        else
        {
            if (transform.position.x > endPosition) transform.position = new Vector3(startPosition, transform.position.y, 0);
            if (transform.position.x < startPosition) transform.position = new Vector3(endPosition, transform.position.y, 0);
        }

        transform.Translate(Vector3.right * SceneManagerButtons.Speed / 10);
    }

    private void OnTriggerEnter2D(Collider2D collision) => StartCoroutine(GoToScene());

    private void OnTriggerExit2D(Collider2D collision) => StopAllCoroutines();

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(3);
        print(scenePath);
        System.Diagnostics.Process.Start(scenePath);
        Application.Quit();
    }

    public IEnumerator LoadAll(string[] filePaths) //Load all video's and textures
    {
        foreach (string filePath in filePaths)
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath);

            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success) Debug.Log(uwr.error);
            else icon = (DownloadHandlerTexture.GetContent(uwr));

            GetComponent<SpriteRenderer>().material.mainTexture = icon;
            //GetComponent<Renderer>().material.SetTexture("_BaseMap", icon); voor pandemic
        }
    }
}