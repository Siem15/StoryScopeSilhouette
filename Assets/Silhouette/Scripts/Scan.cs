using UnityEngine;

public class Scan : MonoBehaviour
{
    public GameObject scanCam;
    public Texture emptySprite;
    public Scan otherSide;
    public bool isEmpty = true;

    Renderer rend;
    CameraSnap cameraSnap;
    FiducialController fiducialController;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        cameraSnap = scanCam.GetComponent<CameraSnap>();
        fiducialController = GetComponent<FiducialController>();
    }

    public void SetEmpty()
    {
        rend.material.SetTexture("_BaseMap", emptySprite);
        isEmpty = true;
    }

    private void Update()
    {
        if (isEmpty == true && fiducialController.m_IsVisible && scanCam.activeSelf)
        {
            isEmpty = false;
            otherSide.SetEmpty();
            ScanDrawing();
        }
    }
    private void ScanDrawing()
    {
        cameraSnap.LoadNew(gameObject);
        cameraSnap.StartSnapping();
    }
}