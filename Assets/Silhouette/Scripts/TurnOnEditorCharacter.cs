using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TurnOnEditorCharacter : MonoBehaviour
{
#if UNITY_EDITOR
    FiducialController fiducialController;
    public bool show = false;

    private void Start()
    {
        fiducialController = GetComponent<FiducialController>();
    }

    void Update()
    {
        if (show)
        {
            fiducialController.enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
#endif
}