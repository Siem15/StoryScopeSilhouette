using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZSceneSelectorManager : MonoBehaviour
{
    public List<string> otherScenesPath = new List<string>();

#if UNITY_STANDALONE_WIN
    string filesLocation = "C:/StoryScopeMedia/Builds/";
#endif
#if UNITY_STANDALONE_LINUX
    string filesLocation = "/home/InteractiveCulture/Builds/";
#endif
#if UNITY_STANDALONE_OSX
    string filesLocation = "";
#endif

    public GameObject prefabSelector;
    float startPos, endPos;
    // Start is called before the first frame update
    void Start()
    {
        otherScenesPath = new List<string>(Directory.GetDirectories(filesLocation));

        float scenes = otherScenesPath.Count;
        if (scenes < 4) SceneManagerButtons.threshold = true;
        else SceneManagerButtons.threshold = false;

        SceneManagerButtons.threshold = scenes < 4 ? true : false;

        for (int i = 0; i < scenes; i++)
        {
            GameObject levelSelect = Instantiate(prefabSelector, transform);
            JZSceneSelector jzSceneSelector = levelSelect.GetComponent<JZSceneSelector>();
            Vector3 tempT = levelSelect.transform.position;

            tempT.x = (-(scenes * levelSelect.transform.localScale.x + (scenes - 1) * 2) / 2) + 4 + (i * 10);

            if (i == 0)
            {
                startPos = tempT.x - 4f;
                endPos = startPos * -1 + 2;
            }

            jzSceneSelector.startPos = startPos;
            jzSceneSelector.endPos = endPos;
            levelSelect.transform.position = tempT;

            jzSceneSelector.pathToImage = otherScenesPath[i];
        }
    }
}

public static class SceneManagerButtons
{
    public static float speed;
    public static bool threshold;
}