using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundAuto : MonoBehaviour
{
    void Start()
    {
        // Найти кнопки, отвечающие за музыку и звуки, чтобы их исключить из общего назначения звука
        var musicBtn = GameObject.Find("Button(Music)");
        var sfxBtn = GameObject.Find("Button(Zvyk)");

        foreach (var btn in FindObjectsOfType<Button>())
        {
            // Пропускаем кнопки, на которых стоит ToggleButtonIcon (они сами всё обработают)
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
