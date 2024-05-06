using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZMicrophone : MonoBehaviour
{
    AudioSource audioSource;
    string lastMic;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastMic = Microphone.devices[Microphone.devices.Length - 1];
    }

    public void StartRecording()
    {
        audioSource.clip = Microphone.Start(lastMic, true, 3, 44100);
    }

    public void StopRecording()
    {
        Microphone.End(lastMic);
        audioSource.Play();
    }
}