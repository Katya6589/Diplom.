using UnityEngine;

public class FirstOknoController : MonoBehaviour
{
    private static bool wasFirstOknoShown = false;

    public GameObject firstOknoPanel;
    public GameObject startMenuPanel;
    public GameObject vsegdaVidnoPanel;

    void Start()
    {
        // Скрываем всё кроме firstOkno
        startMenuPanel.SetActive(false);
        vsegdaVidnoPanel.SetActive(false);

        if (!wasFirstOknoShown)
        {
            firstOknoPanel.SetActive(true);
            wasFirstOknoShown = true;
        }
        else
        {
            firstOknoPanel.SetActive(false);
            // Если First_okno не показывается — сразу открываем Start_menu и VsegdaVidno
            startMenuPanel.SetActive(true);
            vsegdaVidnoPanel.SetActive(true);
        }
    }

    // Вызывается при нажатии кнопки "Закрыть" в First_okno
    public void OnCloseFirstOkno()
    {
        firstOknoPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        vsegdaVidnoPanel.SetActive(true);
    }
}

