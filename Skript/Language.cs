using System.Collections;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using System;

public class Language : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern IntPtr GetLang();

    public string CurrentLanguage;
    [SerializeField] TextMeshProUGUI _languageText;

    public static Language Instance;
    private static bool _languageChecked = false; // <-- флаг "уже определено"

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (_languageChecked)
            {
                // Если язык уже определяли в этой сессии
                if (_languageText != null)
                    _languageText.text = CurrentLanguage;
                return;
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            TryGetLang();
#else
            CurrentLanguage = Application.systemLanguage.ToString().ToLower();
            _languageChecked = true;
            if (_languageText != null)
                _languageText.text = CurrentLanguage;
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private void TryGetLang()
    {
        IntPtr strPtr = GetLang();
        string lang = Marshal.PtrToStringUTF8(strPtr);
        if (string.IsNullOrEmpty(lang))
        {
            CurrentLanguage = "...";
            if (_languageText != null)
                _languageText.text = "Загрузка языка...";
            Invoke(nameof(TryGetLang), 0.5f);
        }
        else
        {
            CurrentLanguage = lang;
            _languageChecked = true;
            if (_languageText != null)
                _languageText.text = CurrentLanguage;
        }
    }
#endif
}
