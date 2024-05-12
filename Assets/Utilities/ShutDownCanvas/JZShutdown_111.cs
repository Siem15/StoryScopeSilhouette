using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class JZShutdown_111 : FiducialController
{
    public ShutdownCor shutdownCor;
    public bool canShutdown = true;
    DateTime onStartUp;

    public override void Start()
    {
        base.Start();
#if !UNITY_EDITOR //TODO: why does UNITY_STANDALONE not work
        startup = DateTime.Now;
        if(PlayerPrefs.HasKey("power") && GetPPPower("power") == "on")
        {
            Debug.Log("Didn't shutdown correctly" + "  Date: " + DateTime.Now);
        }
        SetPPPower("power", "on");
#endif
    }
    public void SetPlayerPrefsPower(string power, string toggle)
    {
#if !UNITY_EDITOR //TODO: why does UNITY_STANDALONE not work
        Debug.Log("Power: " + toggle + "  Date: " + DateTime.Now);
        if (toggle == "off") Debug.Log("On " + DateTime.Now + " ran for " + (DateTime.Now - startup));
        PlayerPrefs.SetString(power, toggle);
        PlayerPrefs.Save();
#endif
    }

    private string GetPlayerPrefsPower(string power) => PlayerPrefs.GetString(power);

    public override void ShowGameObject()
    {
        base.ShowGameObject();

        if (canShutdown)
        {
            canShutdown = false;
            shutdownCor.StartShutdownTimer();
        }
    }

    public override void HideGameObject()
    {
        base.HideGameObject();

        if (!canShutdown)
        {
            canShutdown = true;
            shutdownCor.StopShutdownTimer();
        }
    }
}