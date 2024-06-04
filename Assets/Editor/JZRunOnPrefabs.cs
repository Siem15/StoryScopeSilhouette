using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JZRunOnPrefabs : Editor
{
    [MenuItem("GameObject/Silhouette/addSpriteCode", false, -1)]
    public static void CreateSprites()
    {
        List<Component> renderers = new List<Component>();
        Component[] childRenderers = Selection.activeGameObject.GetComponentsInChildren(typeof(SpriteRenderer));

        Selection.activeGameObject.AddComponent<JZSpriteRoot>();
        Selection.activeGameObject.transform.GetChild(0).gameObject.SetActive(true);

        renderers.AddRange(childRenderers);

        foreach (Component renderer in renderers)
        {
            if (renderer != null && !renderer.gameObject.name.Contains("eye"))
            {
                renderer.gameObject.AddComponent<JZSpriteChild>();
            }
        }
    }
}