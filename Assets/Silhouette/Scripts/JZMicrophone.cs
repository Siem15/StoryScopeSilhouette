using UnityEngine;

/// <summary>
/// 
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