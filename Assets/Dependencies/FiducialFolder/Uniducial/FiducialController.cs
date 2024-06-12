/*
Copyright (c) 2012 André Gröschel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class FiducialController : MonoBehaviour
{
    public int MarkerID = 0;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };

    // Translation...
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;
    public bool grounded = false;
    public bool templateCam = false;
    public bool full = false;

    // Rotation...
    public bool IsRotationMapped = false;
    public bool UseRotation = false;
    public bool AutoHideGO = false;
    private bool m_ControlsGUIElement = false;
    public float DegreesToAction;
    float lastDegree, eulerDegree;
    float rotationDifference;
    float totalRotation;
    public float hideDelay = 1f;

    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    public UniducialLibrary.TuioManager m_TuioManager;
    private Camera m_MainCamera;
    public float camY;
    public float camX;

    [Serializable]
    public class ExampleEvent : UnityEvent { }

    public ExampleEvent onRotateForward = new ExampleEvent();
    public ExampleEvent onRotateBackward = new ExampleEvent();

    private float zAxis;

    // Member variabes...
    public Vector2 m_ScreenPosition;
    private Vector3 m_WorldPosition;
    private Vector2 m_Direction;
    private float m_Angle;
    private float m_AngleDegrees;
    private float m_Speed;
    private float m_Acceleration;
    private float m_RotationSpeed;
    private float m_RotationAcceleration;
    public bool m_IsVisible = false;

    public float RotationMultiplier = 1;

    public virtual void Awake()
    {
#if UNITY_STANDALONE_LINUX
     InvertX = true;
     RotateAround = RotationAxis.Forward;
#endif

        zAxis = transform.position.z;
        m_TuioManager = UniducialLibrary.TuioManager.Instance;

        // Uncomment next line to set port explicitly (default is 3333).
        //tuioManager.TuioPort = 7777;

        m_TuioManager.Connect();

        // Check if game object needs to be transformed in normalized 2D space.
        if (IsAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            m_ControlsGUIElement = true;
        }

        // Initialize member variables.
        m_ScreenPosition = Vector2.zero;
        m_WorldPosition = Vector3.zero;
        m_Direction = Vector2.zero;
        m_Angle = 0f;
        m_AngleDegrees = 0;
        m_Speed = 0f;
        m_Acceleration = 0f;
        m_RotationSpeed = 0f;
        m_RotationAcceleration = 0f;
        m_IsVisible = false; //NOTE: this was 'true'.
    }

    public virtual void Start()
    {
        // Check if the main camera exists and if so, store in reference.
        if (!GameObject.FindGameObjectWithTag("MainCamera").TryGetComponent(out m_MainCamera))
        {
            Debug.LogError("There is no main camera defined in your scene.");
        }

        // NOTE: reference to manager is used nowhere in function.
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public virtual void Update()
    {
        if (UseRotation)
        {
            GetRotation();
        }

        if (m_TuioManager.IsConnected && m_TuioManager.IsMarkerAlive(MarkerID))
        {
            TUIO.TuioObject marker = m_TuioManager.GetMarker(MarkerID);

            // Update parameters
            m_ScreenPosition.x = marker.getX();
            m_ScreenPosition.y = marker.getY();
            m_Angle = marker.getAngle() * RotationMultiplier;
            m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;
            m_Speed = marker.getMotionSpeed();
            m_Acceleration = marker.getMotionAccel();
            m_RotationSpeed = marker.getRotationSpeed() * RotationMultiplier;
            m_RotationAcceleration = marker.getRotationAccel();
            m_Direction.x = marker.getXSpeed();
            m_Direction.y = marker.getYSpeed();
            m_IsVisible = true;

            // Set game object to visible, if it was hidden before
            ShowGameObject();

            // Update transform component
            UpdateTransform();
        }
        else 
        {
            // Automatically hide game object when marker is not visible.
            if (AutoHideGO)
            {
                StartCoroutine(HideDelay(hideDelay));
            }
        }
    }

    IEnumerator HideDelay(float timeOfDelay)
    {
        yield return new WaitForSeconds(timeOfDelay);
        HideGameObject();
        yield return null;
    }

    void OnApplicationQuit() 
    { 
        if (m_TuioManager.IsConnected)
        {
            m_TuioManager.Disconnect();
        }
    }

    public virtual void UpdateTransform()
    {
        // Position mapping...
        if (IsPositionMapped)
        {
            // Calculate world position with respect to camera view direction.
            float xPosition = m_ScreenPosition.x;
            float yPosition = m_ScreenPosition.y;

            if (InvertX)
            {
                xPosition = 1 - xPosition;
            }

            if (InvertY)
            {
                yPosition = 1 - yPosition;
            }

            if (m_ControlsGUIElement) 
            {
                transform.position = new Vector3(xPosition, 1 - yPosition, 0);
            }
            else
            {
                Vector3 position = new Vector3(xPosition * Screen.width, (1 - yPosition) * Screen.height, CameraOffset);
                m_WorldPosition = m_MainCamera.ScreenToWorldPoint(position);
                //worldPosition += cameraOffset * mainCamera.transform.forward;
                m_WorldPosition = new Vector3(m_WorldPosition.x, m_WorldPosition.y, zAxis);
                transform.position = m_WorldPosition;
            }
        }

        // Rotation mapping...
        if (IsRotationMapped)
        {
            Quaternion rotation = Quaternion.identity;

            switch (RotateAround)
            {
                case RotationAxis.Forward:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.forward);
                    break;
                case RotationAxis.Back:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.back);
                    break;
                case RotationAxis.Up:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.up);
                    break;
                case RotationAxis.Down:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.down);
                    break;
                case RotationAxis.Left:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.left);
                    break;
                case RotationAxis.Right:
                    rotation = Quaternion.AngleAxis(m_AngleDegrees, Vector3.right);
                    break;
            }

            transform.rotation = rotation;
        }
    }

    private void GetRotation()
    {
        float eulers = m_AngleDegrees;

        if (eulers != eulerDegree)
        {
            rotationDifference = eulerDegree - eulers;
            if (Mathf.Abs(rotationDifference) < 50) totalRotation += rotationDifference;
            eulerDegree = eulers;
        }

        if (totalRotation > lastDegree + DegreesToAction)
        {
            lastDegree = totalRotation;
            onRotateForward.Invoke();
        }

        if (totalRotation < lastDegree - DegreesToAction)
        {
            lastDegree = totalRotation;
            onRotateBackward.Invoke();
        }
    }

    public virtual void ShowGameObject()
    {
        StopAllCoroutines();

        if (m_ControlsGUIElement)
        {
            // Show GUI components.
            Text text = gameObject.GetComponent<Text>();
            Image image = gameObject.GetComponent<Image>();

            if (text != null && !text.enabled)
            {
                text.enabled = true;
            }
            
            if (image != null && !image.enabled)
            {
                image.enabled = true;
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            Renderer renderer = gameObject.GetComponent<Renderer>();
            Camera camera = gameObject.GetComponent<Camera>();

            if (renderer != null && !renderer.enabled)
            {
                renderer.enabled = true;
            }

            if (camera != null && !camera.enabled)
            {
                camera.enabled = true;
            }
        }
    }

    public virtual void HideGameObject()
    {
        if (!AutoHideGO)
        {
            return;
        }

        m_IsVisible = false;

        if (m_ControlsGUIElement)
        {
            // Hide GUI components.
            Text text = gameObject.GetComponent<Text>();
            Image image = gameObject.GetComponent<Image>();

            if (text != null && text.enabled)
            {
                text.enabled = false;
            }

            if (image != null && image.enabled)
            {
                image.enabled = false;
            }
        }
        else
        {
            // Set 3D game object to visible, if it was hidden before.
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            Renderer renderer = gameObject.GetComponent<Renderer>();
            Camera camera = gameObject.GetComponent<Camera>();

            if (renderer != null && renderer.enabled)
            {
                renderer.enabled = false;
            }

            if (camera != null && camera.enabled)
            {
                camera.enabled = false;
            }
        }
    }

    #region Getters
    public bool IsAttachedToGUIComponent() => gameObject.GetComponent<Text>() != null || gameObject.GetComponent<Image>() != null;

    public Vector2 ScreenPosition => m_ScreenPosition;

    public Vector3 WorldPosition => m_WorldPosition;

    public Vector2 MovementDirection => m_Direction;

    public float Angle => m_Angle;

    public float AngleDegrees => m_AngleDegrees;

    public float Speed => m_Speed;

    public float Acceleration => m_Acceleration;

    public float RotationSpeed => m_RotationSpeed;

    public float RotationAcceleration => m_RotationAcceleration;

    public bool IsVisible => m_IsVisible;
    #endregion
}