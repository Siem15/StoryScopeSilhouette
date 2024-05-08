using UnityEngine;

/// <summary>
/// This script creates a ScriptableObject called CameraActive that currently only contains a 
/// boolean called CamIsActive that gets referenced in 2 other scripts. This is used to check if the webcam is turned on.
/// 
/// I currently don't really understand why this is in a separate script, seems a bit redundant
/// - Siem Wesseling, 08/05/2024
/// /// </summary>

[CreateAssetMenu(fileName = "CameraActive", menuName = "ScriptableObjects/IsCamActive", order = 1)]
public class JZSOCameraActive : ScriptableObject
{
    public bool camIsActive;
}