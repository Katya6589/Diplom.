using System.Runtime.InteropServices;
using UnityEngine;

public class my : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void LoadingReady();
    [DllImport("__Internal")]
    private static extern void GameplayStart();
    [DllImport("__Internal")]
    private static extern void GameplayStop();
    [DllImport("__Internal")]
    private static extern System.IntPtr GetLang();
#endif

    public void SignalLoadingReady()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadingReady();
#endif
    }

    public void SignalGameplayStart()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GameplayStart();
#endif
    }

    public void SignalGameplayStop()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GameplayStop();
#endif
    }

    public string GetPlatformLanguage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        var ptr = GetLang();
        return System.Runtime.InteropServices.Marshal.PtrToStringUTF8(ptr);
#else
        return Application.systemLanguage.ToString().ToLower();
#endif
    }
}
