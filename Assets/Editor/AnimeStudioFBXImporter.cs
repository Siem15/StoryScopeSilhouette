using System;
using UnityEditor;
using UnityEngine;

public class AnimeStudioPostProcessor : AssetPostprocessor
{
    private bool IsAnimeStudioModel = false;

    private void OnPreprocessModel()
    {
        IsAnimeStudioModel = false;

        // resampleRotations only became part of Unity as of version 5.3.
        // If you're using an older version of Unity, comment out the following block of code.
        // Set resampleRotations to false to fix the "bouncy" handling of constant interpolation keyframes.
        try
        {
            ModelImporter importer = assetImporter as ModelImporter;
            importer.resampleCurves = false;
        }
        catch
        {

        }
    }

    private void OnPostprocessGameObjectWithUserProperties(GameObject g, string[] names, object[] values)
    {
        // Only operate on FBX files
        if (assetPath.IndexOf(".fbx") == -1)
        {
            return;
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] == "ASP_FBX")
            {
                IsAnimeStudioModel = true; // at least some part of this comes from Anime Studio
                break;
            }
        }
    }

    private void OnPostprocessModel(GameObject g)
    {
        // Only operate on FBX files
        if (assetPath.IndexOf(".fbx") == -1)
        {
            return;
        }

        if (!IsAnimeStudioModel)
        {
            //Debug.Log("*** Not Moho ***");
            return;
        }

        Shader shader = Shader.Find("Sprites/Default");

        if (shader == null)
        {
            return;
        }

        Renderer[] renderers = g.GetComponentsInChildren<Renderer>();
        int straightRenderOrder = shader.renderQueue;

        foreach (Renderer renderer in renderers)
        {
            int renderOrder = straightRenderOrder;

            if (renderer.name.Contains("|"))
            {
                string[] stringSeparators = ["|"];
                string[] parts = renderer.name.Split(stringSeparators, StringSplitOptions.None);

                if (int.TryParse(parts[parts.Length - 1], out int j))
                {
                    renderOrder += j;
                }
            }

            renderer.sharedMaterial.shader = shader; // apply an unlit shader
            renderer.sharedMaterial.renderQueue = renderOrder; // set a fixed render order
            straightRenderOrder++;
        }
    }
}