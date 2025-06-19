using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundAuto : MonoBehaviour
{
    void Start()
    {
        // ����� ������, ���������� �� ������ � �����, ����� �� ��������� �� ������ ���������� �����
        var musicBtn = GameObject.Find("Button(Music)");
        var sfxBtn = GameObject.Find("Button(Zvyk)");

        foreach (var btn in FindObjectsOfType<Button>())
        {
            // ���������� ������, �� ������� ����� ToggleButtonIcon (��� ���� �� ����������)
            if ((musicBtn && btn == musicBtn.GetComponent<Button>()) ||
                (sfxBtn && btn == sfxBtn.GetComponent<Button>()))
                continue;

            btn.onClick.AddListener(() =>
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayMenuClick();
            });
        }
    }
}
