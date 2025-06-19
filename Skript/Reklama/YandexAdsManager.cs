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

    private const float adCooldown = 60f; // �������� ����� �������� � �������� (1 ������)
    private float lastAdTime = -1000f;    // ��������� ����� ������ ������� (Time.realtimeSinceStartup)

    void Awake()
    {
        // ��������
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
    /// ���������� ������� ������ ���� ������ ������ ������ � �������� ������.
    /// ���� ��� � ����� �������� ������ (��������, ������� ������).
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
    /// ���������� ������������� ������� (��� �������� �������).
    /// </summary>
    public void ShowFullscreen(System.Action onClosed)
    {
        _onClosed = onClosed;
        AudioManager.Instance?.PauseAllAudio(); // <--- ������ ��� ������!
#if UNITY_WEBGL && !UNITY_EDITOR
    ShowFullscreenAd();
#else
        _onClosed?.Invoke();
#endif
    }

    public void OnAdClosed()
    {
        AudioManager.Instance?.ResumeAllAudio(); // <--- ������ ��� ������!
        _onClosed?.Invoke();
    }

}
