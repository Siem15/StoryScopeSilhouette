using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CameraSnap : MonoBehaviour
{
    Renderer mainRenderer; // Main renderer for new game object.
    Renderer[] childRenderers; // Child renderers for new game object.
    public float tolerance; // Flood fill tolerance(?).
    Camera cam; // Used to change depth whilst reading screenshot(?).  

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void StartSnapping()
    {
        // Start coroutine to read screenshot.
        StartCoroutine(Screenshot());
    }
    
    public IEnumerator Screenshot()
    {
        // TODO: eventueel aftellen...

        cam.depth = 1;
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.FloodFillArea(10, height / 2, Color.clear, tolerance);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();

        //topRenderer.sharedMaterial.mainTexture = tex;
        mainRenderer.material.SetTexture("_BaseMap", tex);

        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (childRenderers[i].name != "eye")
            {
                childRenderers[i].sharedMaterial.mainTexture = tex;
                childRenderers[i].material.SetTexture("_BaseMap", tex);
                //childRenderers[i].sharedMaterial.color = Color.black;
            }
        }

        cam.depth = -5;
        yield return null;
    }
    
    public void LoadNewDrawing(GameObject gameObject)
    {
        // Use drawing object from scan and load all child renderers.
        mainRenderer = gameObject.GetComponent<Renderer>();
        childRenderers = GetChildRenderers(mainRenderer, true); 
        //childRenderers = GetChildRenderers();
    }
    
    // NOTE: until alternative method has been proven to work, DO NOT DELETE THIS FUNCTION!!!
    private Renderer[] GetChildRenderers(Renderer parentRenderer, bool isRecursive)
    {
        // Initialize empty list of renderers.
        List<Renderer> items = new List<Renderer>();

        for (int i = 0; i < parentRenderer.transform.childCount; i++)
        {
            // Get child renderers of parent renderer.
            Renderer childRenderer = parentRenderer.transform.GetChild(i).gameObject.GetComponent<Renderer>();

            // Add child renderers to list.
            items.Add(childRenderer);

            if (isRecursive)
            {
                // Use recursion to get child renderers of child renderers and add to list.
                items.AddRange(GetChildRenderers(childRenderer, isRecursive));
            }
        }

        // Convert list to array and return.
        return items.ToArray();
    }

    private Renderer[] GetChildRenderers()
    {
        // Initialize empty list of renderers.
        List<Renderer> items = new List<Renderer>();

        // Get child renderers of parent renderer.
        Renderer[] childRenderers = mainRenderer.GetComponentsInChildren<Renderer>();
        items.AddRange(childRenderers);

        foreach (Renderer renderer in childRenderers)
        {
            // Use foreach-loop to get sub-renderers from child renderers.
            Renderer[] subRenderers = renderer.GetComponentsInChildren<Renderer>();
            items.AddRange(subRenderers);
        }

        // Convert list to array and return.
        return items.ToArray();
    }
}