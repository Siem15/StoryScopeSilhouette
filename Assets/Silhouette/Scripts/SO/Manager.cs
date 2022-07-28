using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public Material ghostMaterial;
    public Texture [] ghostTextures;

    public GameObject[] InstantiatieThis;


    void Start()
    {
        for (int i = 0; i <  InstantiatieThis.Length; i++)
        {
            Instantiate(InstantiatieThis[i]);
        }

    }

    private void Update()
    {
        if (ghostTextures.Length == 0) return;
        ghostMaterial.mainTexture = ghostTextures[JZSpriteCounter.spriteCounter % ghostTextures.Length];
    }
}
