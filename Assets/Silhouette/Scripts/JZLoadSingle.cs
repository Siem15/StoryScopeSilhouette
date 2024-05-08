using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// In essence this script does the same thing that JZLoadFromExternalV2 does, but it can only load in images. 
/// 
/// - Siem Wesseling, 08/05/2024
/// </summary>

public class JZLoadSingle : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    string filesLocationOG = @"C:/StoryScopeMedia/Props";
    string filesLocation = @"C:/StoryScopeMedia/Props";
#endif

#if UNITY_STANDALONE_LINUX
    string filesLocation = "/home/InteractiveCulture/StoryScopeMedia/Props";
    // string filesLocation = "/home/jzi7/Desktop/Characters";
#endif

#if UNITY_STANDALONE_OSX
    string filesLocation = "";
#endif

    public Texture2D image;

    private void OnEnable()
    {
        StartCoroutine(GetExternalImages(Directory.GetFiles(filesLocation)));
    }

    public IEnumerator GetExternalImages(string[] filePaths) //Load paths 
    {
        foreach (string filePath in filePaths) //check img to texturelist
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + filePath);

            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success) Debug.Log(uwr.error);
            else AddImgFile(filePath, uwr);
        }
    }

    private void AddImgFile(string filePath, UnityWebRequest uwr)
    {
        if (filePath.ToLower().Contains(gameObject.name.ToLower())) image = (DownloadHandlerTexture.GetContent(uwr));
        GetComponent<Renderer>().material.mainTexture = image;
    }
}