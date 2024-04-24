using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JZRunOnPrefabs : Editor
{
    [MenuItem("GameObject/Silhouette/addSpriteCode", false, -1)]
    public static void SendSprites()
    {
        List<Component> _renderers = new List<Component>();

        Component[] _childRenderers = Selection.activeGameObject.GetComponentsInChildren(typeof(SpriteRenderer));
        Selection.activeGameObject.AddComponent<JZSpriteRoot>();
        Selection.activeGameObject.transform.GetChild(0).gameObject.SetActive(true);
        _renderers.AddRange(_childRenderers);

        AddScripts(_renderers);
    }

    private static void AddScripts(List<Component> _renderers)
    {
        foreach (var rend in _renderers)
        {
            if (rend != null && !rend.gameObject.name.Contains("eye"))
            {
                rend.gameObject.AddComponent<JZSpriteChild>();
            }
        }
    }
}