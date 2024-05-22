using UnityEngine;

/// <summary>
/// This script searches the system for a microphone and creates functions to record said microphone.
/// 
/// - Siem Wesseling, 13/05/2024
/// </summary>

public class JZMicrophone : MonoBehaviour
{
    AudioSource audioSource;
    string lastMicrophone;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastMicrophone = Microphone.devices[Microphone.devices.Length - 1];
    }

    public void StartRecording()
    {
        audioSource.clip = Microphone.Start(lastMicrophone, true, 3, 44100);
    }

    public void StopRecording()
    {
        Microphone.End(lastMicrophone);
        audioSource.Play();
    }
}