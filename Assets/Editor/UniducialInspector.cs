/*
Copyright (c) 2012 Andr� Gr�schel

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

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FiducialController))]
public class UniducialInspector : Editor
{
    // Used to store the fiducial controller of the object
    private FiducialController fiducialController;

    public override void OnInspectorGUI()
    {
        fiducialController = target as FiducialController;
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        EditorGUILayout.TextField("Game Object", EditorStyles.boldLabel); // Header attribute

        EditorGUILayout.BeginHorizontal(); // Begin horizontal UI group to show variables
        fiducialController.MarkerID = EditorGUILayout.IntField("Marker ID", fiducialController.MarkerID);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        fiducialController.AutoHideGO = EditorGUILayout.Toggle("Auto-Hide GameObject", fiducialController.AutoHideGO);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(); // Put some space between previous and next field
        EditorGUILayout.TextField("Camera", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        fiducialController.camX = EditorGUILayout.FloatField("Camera X", fiducialController.camX);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        fiducialController.camY = EditorGUILayout.FloatField("Camera Z", fiducialController.camY);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.TextField("Delay", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        fiducialController.hideDelay = EditorGUILayout.FloatField("Hide Delay", fiducialController.hideDelay);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.TextField("Position", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        fiducialController.IsPositionMapped = EditorGUILayout.Toggle("Control Position", fiducialController.IsPositionMapped);
        EditorGUILayout.EndHorizontal();

        // Use if-statement check on fiducial controller to create drop-down fields
        if (fiducialController.IsPositionMapped)
        {
            EditorGUILayout.BeginHorizontal();
            fiducialController.InvertX = EditorGUILayout.Toggle("Invert X-Axis", fiducialController.InvertX);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fiducialController.InvertY = EditorGUILayout.Toggle("Invert Y-Axis", fiducialController.InvertY);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fiducialController.grounded = EditorGUILayout.Toggle("Grounded", fiducialController.grounded);
            EditorGUILayout.EndHorizontal();

            if (!mainCamera.orthographic && !fiducialController.IsAttachedToGUIComponent())
            {
                EditorGUILayout.BeginHorizontal();
                fiducialController.CameraOffset = EditorGUILayout.Slider("Camera offset", 
                    fiducialController.CameraOffset, mainCamera.nearClipPlane, mainCamera.farClipPlane);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.Space();

        EditorGUILayout.TextField("Rotation", EditorStyles.boldLabel);

        if (!fiducialController.IsAttachedToGUIComponent())
        {
            EditorGUILayout.BeginHorizontal();
            fiducialController.IsRotationMapped = EditorGUILayout
                .Toggle("Control Rotation", fiducialController.IsRotationMapped);
            EditorGUILayout.EndHorizontal();

            if (fiducialController.IsRotationMapped)
            {
                EditorGUILayout.BeginHorizontal();
                fiducialController.RotateAround = (FiducialController.RotationAxis)EditorGUILayout
                    .EnumPopup("Rotation Axis", fiducialController.RotateAround);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                fiducialController.RotationMultiplier = EditorGUILayout
                    .Slider("Rotation Factor", fiducialController.RotationMultiplier, 0.01f, 5f);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            fiducialController.UseRotation = EditorGUILayout.Toggle("Use Rotation", fiducialController.UseRotation);
            EditorGUILayout.EndHorizontal();

            if (fiducialController.UseRotation)
            {
                EditorGUILayout.BeginHorizontal();
                fiducialController.DegreesToAction = EditorGUILayout
                    .FloatField("Degrees To Action", fiducialController.DegreesToAction);
                EditorGUILayout.EndHorizontal();

                serializedObject.Update();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onRotateForward"), true);
                serializedObject.ApplyModifiedProperties();

                serializedObject.Update();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onRotateBackward"), true);
                serializedObject.ApplyModifiedProperties();
            }
        }

        if (GUI.changed) 
        {
            EditorUtility.SetDirty(fiducialController);
        }        
    }
}