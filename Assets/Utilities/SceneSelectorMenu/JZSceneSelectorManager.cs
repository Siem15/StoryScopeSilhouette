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
    readonly string filesLocation = "/home/InteractiveCulture/Builds/";
#endif
#if UNITY_STANDALONE_OSX
    readonly string filesLocation = "";
#endif

    public GameObject prefabSelector;
    float startPosition, endPosition;

    // Start is called before the first frame update
    void Start()
    {
        otherScenesPath = new List<string>(Directory.GetDirectories(filesLocation));

        float numOfScenes = otherScenesPath.Count;
        SceneManagerButtons.Threshold = numOfScenes < 4;

        for (int i = 0; i < numOfScenes; i++)
        {
            GameObject levelSelectorObject = Instantiate(prefabSelector, transform);
            JZSceneSelector jzSceneSelector = levelSelectorObject.GetComponent<JZSceneSelector>();
            Vector3 tempTransform = levelSelectorObject.transform.position;

            tempTransform.x = (-(numOfScenes * levelSelectorObject.transform.localScale.x 
                + (numOfScenes - 1) * 2) / 2) + 4 + (i * 10);

            if (i == 0)
            {
                startPosition = tempTransform.x - 4f;
                endPosition = startPosition * -1 + 2;
            }

            jzSceneSelector.startPosition = startPosition;
            jzSceneSelector.endPosition = endPosition;
            levelSelectorObject.transform.position = tempTransform;

            jzSceneSelector.pathToImage = otherScenesPath[i];
        }
    }
}

public static class SceneManagerButtons
{
    public static float Speed;
    public static bool Threshold;
}