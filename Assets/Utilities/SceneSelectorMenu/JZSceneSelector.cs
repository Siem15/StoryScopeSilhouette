using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JZSceneSelector : MonoBehaviour
{
    public string pathToImage;
    public float speed;
    public float startPos, endPos;
    public Texture2D icon;
    string pathToScene;
    private void Start()
    {
#if UNITY_STANDALONE_WIN
        pathToScene = pathToImage + "/StoryScope.exe";
#endif
#if UNITY_STANDALONE_LINUX
        pathToScene = pathToImage + "/StoryScope.x86_64";
#endif

        StartCoroutine(LoadAll(Directory.GetFiles(pathToImage + "/icon/")));
    }
    private void Update()
    {
        if (SceneManagerButtons.threshold)
        {
            if (transform.position.x > endPos) transform.position = new Vector3(endPos, transform.position.y, 0);
            if (transform.position.x < startPos) transform.position = new Vector3(startPos, transform.position.y, 0);
        }
        else
        {
            if (transform.position.x > endPos) transform.position = new Vector3(startPos, transform.position.y, 0);
            if (transform.position.x < startPos) transform.position = new Vector3(endPos, transform.position.y, 0);
        }
        transform.Translate(Vector3.right * SceneManagerButtons.speed / 10);
    }

    private void OnTriggerEnter2D(Collider2D collision) { StartCoroutine(GoToScene()); }

    private void OnTriggerExit2D(Collider2D collision) { StopAllCoroutines(); }

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(3);
        print(pathToScene);
        System.Diagnostics.Process.Start(pathToScene);
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
