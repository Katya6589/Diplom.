using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject _oknoProigrisha, _oknoVigrisha, _oknoIgrovoe;
    public GameObject noCoinsWindow, confirmPurchaseWindow;

    private bool isGameFinished = false; // <-- ����� ����

    void Awake()
    {
        Instance = this;
    }

    public void OnGameFinished(bool win)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FindObjectOfType<my>().SignalGameplayStop();
#endif
        if (isGameFinished) return; // ���� ��� ���������� ����, ������ �� ������
        isGameFinished = true;      // ��������� ��������� ������

        _oknoIgrovoe.SetActive(false);
        if (win)
            _oknoVigrisha.SetActive(true);
        else
            _oknoProigrisha.SetActive(true);
    }

    public void ResetGameFinished()
    {
        isGameFinished = false;
    }
}
