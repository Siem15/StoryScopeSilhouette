using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShutdownCor : MonoBehaviour
{
    public JZShutdown_111 jz;
    public Text text;
    readonly int shutdownTimer = 10;
    int shutdownValue = 10;
    void Start()
    {
        text.enabled = false;
        shutdownValue = shutdownTimer;

#if UNITY_STANDALONE_WIN
        Cursor.visible = false;
#endif
#if UNITY_STANDALONE_LINUX
        Cursor.visible = false;
#endif
#if UNITY_EDITOR
        Cursor.visible = true;
#endif
    }

    public IEnumerator Shutdown()
    {
        shutdownValue = shutdownTimer;
        while (shutdownValue > 0)
        {
            text.text = shutdownValue.ToString();
            yield return new WaitForSeconds(1);
            shutdownValue--;
        }
        jz.SetPPPower("power", "off");
#if UNITY_STANDALONE_WIN
        System.Diagnostics.Process.Start("C:/StoryScopeShutdown.bat");
#endif
#if UNITY_STANDALONE_LINUX
        System.Diagnostics.Process.Start("/home/shutdown.sh");
#endif
    }

    public void StartShutdownTimer()
    {
        StartCoroutine(Shutdown());
    }
    public void StopShutdownTimer()
    {
        StopAllCoroutines();
        text.enabled = false;
    }
}
