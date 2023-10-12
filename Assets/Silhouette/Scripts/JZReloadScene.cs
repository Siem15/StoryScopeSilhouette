using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class JZReloadScene : MonoBehaviour
{

    FiducialController fidu;
    // Start is called before the first frame update
    void Start()
    {
        fidu = GetComponent<FiducialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fidu.IsVisible ) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
}
