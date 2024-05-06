using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class Manager : MonoBehaviour
{
    public Material ghostMaterial;
    public Texture[] ghostTextures;
    public GameObject[] InstantiatieThis;

    private void Start()
    {
#if UNITY_EDITOR
        Application.runInBackground = true;
#endif

        for (int i = 0; i < InstantiatieThis.Length; i++)
        {
            Instantiate(InstantiatieThis[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        if (ghostTextures.Length == 0) return;
        ghostMaterial.mainTexture = ghostTextures[JZSpriteCounter.spriteCounter % ghostTextures.Length];
    }
}