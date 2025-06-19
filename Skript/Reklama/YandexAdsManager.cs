using UnityEngine;
using System.Runtime.InteropServices;

public class YandexAdsManager : MonoBehaviour
{
    public static YandexAdsManager Instance;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();
#endif

    private System.Action _onClosed;

    private const float adCooldown = 60f; // интервал между показами в секундах (1 минута)
    private float lastAdTime = -1000f;    // последнее время показа рекламы (Time.realtimeSinceStartup)

    void Awake()
    {
        // Синглтон
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Показывает рекламу только если прошло больше минуты с прошлого показа.
    /// Если нет — сразу вызывает колбэк (например, рестарт уровня).
    /// </summary>
    public void ShowFullscreenWithCooldown(System.Action onClosed)
    {
        float time = Time.realtimeSinceStartup;
        if (time - lastAdTime >= adCooldown)
        {
            ShowFullscreen(onClosed);
            lastAdTime = time;
        }
        else
        {
            onClosed?.Invoke();
        }
    }

    /// <summary>
    /// Показывает полноэкранную рекламу (без проверки таймера).
    /// </summary>
    public void ShowFullscreen(System.Action onClosed)
    {
        _onClosed = onClosed;
        AudioManager.Instance?.PauseAllAudio(); // <--- Добавь эту строку!
#if UNITY_WEBGL && !UNITY_EDITOR
    ShowFullscreenAd();
#else
        _onClosed?.Invoke();
#endif
    }

    public void OnAdClosed()
    {
        AudioManager.Instance?.ResumeAllAudio(); // <--- Добавь эту строку!
        _onClosed?.Invoke();
    }

}
