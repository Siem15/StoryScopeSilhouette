using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class JZSpriteRoot : MonoBehaviour
{
    //TODO: Turned off stuff is the ligth version 
#if UNITY_STANDALONE_WIN
    string filesLocationOG = @"C:/StoryScopeMedia/SheetsOG";
    string filesLocation = @"C:/StoryScopeMedia/Sheets";
#endif

#if UNITY_STANDALONE_LINUX
    string filesLocation = "/home/InteractiveCulture/StoryScopeMedia/Sheets";
    // string filesLocation = "/home/jzi7/Desktop/Characters";
#endif

#if UNITY_STANDALONE_OSX
    string filesLocation = "";
#endif

    public List<Texture2D> images = new List<Texture2D>();

    private void OnEnable()
    {
        images = Resources.LoadAll<Texture2D>("Character/" + gameObject.name).ToList();
        //TODO:  StartCoroutine(GetExternalImages(Directory.GetFiles(filesLocation)));
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
        if (filePath.ToLower().Contains(gameObject.name.ToLower())) images.Add(DownloadHandlerTexture.GetContent(uwr));
    }
}