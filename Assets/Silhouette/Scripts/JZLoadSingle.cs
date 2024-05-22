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
        // Get external image files stored on computer.
        StartCoroutine(GetExternalImages(Directory.GetFiles(filesLocation)));
    }

    public IEnumerator GetExternalImages(string[] filePaths)
    {
        // Load image files from file paths.
        foreach (string filePath in filePaths)
        {
            UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture("file:///" + filePath);
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(unityWebRequest.error);
            }
            else 
            {
                AddImageFile(filePath, unityWebRequest);
            }            
        }
    }

    private void AddImageFile(string filePath, UnityWebRequest unityWebRequest)
    {
        // Get texture file for object if name is included in file path.
        if (filePath.ToLower().Contains(gameObject.name.ToLower())) 
        {
            image = (DownloadHandlerTexture.GetContent(unityWebRequest));
        }
        
        // Set main texture to downloaded image.
        GetComponent<Renderer>().material.mainTexture = image;
    }
}