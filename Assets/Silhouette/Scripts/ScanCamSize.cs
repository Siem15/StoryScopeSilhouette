using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ScanCamSize : MonoBehaviour
{
    new Camera camera;
    public GameObject LinksOnder;
    public GameObject RechtsBoven;
    public GameObject LinksBoven;
    public GameObject RechtsOnder;
    float camSize;
    public float growRate;
    private LineRenderer lineRenderer;

    FiducialController fiducialController;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        camera = GetComponent<Camera>();
        fiducialController = GetComponent<FiducialController>();
        ResetBorders();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1f, 8f);
        if (camSize != camera.orthographicSize) ResetBorders();
        if(fiducialController.RotationSpeed > 0) camera.orthographicSize += growRate;
        if (fiducialController.RotationSpeed < 0) camera.orthographicSize -= growRate;
    }
    
    private void ResetBorders()
    {
        camSize = camera.orthographicSize;
        Vector3[] positions = new Vector3[4];

        positions[0] = camera.ScreenToWorldPoint(new Vector3(0, 0, 13)); //LnksOnder
        LinksOnder.transform.position = positions[0];
        positions[2] = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 13)); //Rechtsboven
        RechtsBoven.transform.position = positions[2];
        positions[1] = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 13)); //LnksBoven
        LinksBoven.transform.position = positions[1];
        positions[3] = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 13)); //Rechtsboven
        RechtsOnder.transform.position = positions[3];

        RenderLines(positions);

    }

    private void RenderLines(Vector3[] points)
    {
        lineRenderer.startWidth = .2f;
        lineRenderer.endWidth = .2f;
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
        lineRenderer.loop = true;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }
}


  