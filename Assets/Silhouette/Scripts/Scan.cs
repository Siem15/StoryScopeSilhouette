using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public GameObject scanCam;
    FiducialController fiducialController;
    public GameObject otherSide;
    public bool isEmpty = true;
    public CameraSnap cameraSnap;
    public Texture emptySprite;

    private void Awake()
    {
        GetComponent<Renderer>().material.SetTexture("_BaseMap", emptySprite);
        scanCam = GameObject.FindGameObjectWithTag("ScanCam");
        cameraSnap = scanCam.GetComponent<CameraSnap>();
       // cameraSnap = GameObject.FindGameObjectWithTag("ScanCam").GetComponent<CameraSnap>();
        fiducialController = GetComponent<FiducialController>();
    }
    private void Update()
    {
        if(scanCam == null) scanCam = GameObject.FindGameObjectWithTag("ScanCam");
        if (Input.GetKeyDown(KeyCode.L))
        {
            ScanDrawing();
        }

        if (scanCam.activeSelf)
        {
            if (fiducialController.m_IsVisible && isEmpty == true)
            {
                isEmpty = false;
                PurgeOtherSide();
                ScanDrawing();
            }
        }
    }

    private void PurgeOtherSide()
    {
        otherSide.GetComponent<Renderer>().material.SetTexture("_BaseMap", emptySprite); //clear otherside
        otherSide.GetComponent<Scan>().isEmpty = true; //set isEmpty true
    }

    private void ScanDrawing()
    {
        cameraSnap.LoadNew(gameObject);
        cameraSnap.StartSnapping();
    }
}
