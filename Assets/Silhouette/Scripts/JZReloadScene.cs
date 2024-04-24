using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (fidu.IsVisible) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}