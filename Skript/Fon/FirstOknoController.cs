using UnityEngine;

public class FirstOknoController : MonoBehaviour
{
    private static bool wasFirstOknoShown = false;

    public GameObject firstOknoPanel;
    public GameObject startMenuPanel;
    public GameObject vsegdaVidnoPanel;

    void Start()
    {
        // �������� �� ����� firstOkno
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
            // ���� First_okno �� ������������ � ����� ��������� Start_menu � VsegdaVidno
            startMenuPanel.SetActive(true);
            vsegdaVidnoPanel.SetActive(true);
        }
    }

    // ���������� ��� ������� ������ "�������" � First_okno
    public void OnCloseFirstOkno()
    {
        firstOknoPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        vsegdaVidnoPanel.SetActive(true);
    }
}

