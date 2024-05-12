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
    readonly string buildsFilePath = "C:/StoryScopeMedia/Builds/";
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
        otherScenesPath = new List<string>(Directory.GetDirectories(buildsFilePath));

        float scenes = otherScenesPath.Count;
        if (scenes < 4) SceneManagerButtons.threshold = true;
        else SceneManagerButtons.threshold = false;

        SceneManagerButtons.threshold = scenes < 4 ? true : false;

        for (int i = 0; i < scenes; i++)
        {
            GameObject levelSelectorObject = Instantiate(prefabSelector, transform);
            JZSceneSelector jzSceneSelector = levelSelectorObject.GetComponent<JZSceneSelector>();
            Vector3 tempTransform = levelSelectorObject.transform.position;

            tempTransform.x = (-(scenes * levelSelectorObject.transform.localScale.x + (scenes - 1) * 2) / 2) + 4 + (i * 10);

            if (i == 0)
            {
                startPos = tempTransform.x - 4f;
                endPos = startPos * -1 + 2;
            }

            jzSceneSelector.startPos = startPos;
            jzSceneSelector.endPos = endPos;
            levelSelectorObject.transform.position = tempTransform;

            jzSceneSelector.pathToImage = otherScenesPath[i];
        }
    }
}

public static class SceneManagerButtons
{
    public static float speed;
    public static bool threshold;
}