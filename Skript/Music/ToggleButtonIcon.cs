using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonIcon : MonoBehaviour
{
    public enum Mode { Music, Sfx }
    public Mode mode;

    public GameObject onIconObject;   // ������ ���������� ������ (��������, IconOn)
    public GameObject offIconObject;  // ������ ����������� ������ (��������, IconOff)
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        UpdateIcon();

        button.onClick.AddListener(() =>
        {
            if (mode == Mode.Music)
                AudioManager.Instance.ToggleMusic();
            else
                AudioManager.Instance.ToggleSfx();

            UpdateIcon();
        });
    }

    void UpdateIcon()
    {
        bool isOn = (mode == Mode.Music) ? AudioManager.Instance.IsMusicOn() : AudioManager.Instance.IsSfxOn();
        if (onIconObject) onIconObject.SetActive(isOn);
        if (offIconObject) offIconObject.SetActive(!isOn);
    }
}
