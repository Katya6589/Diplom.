using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject _oknoProigrisha, _oknoVigrisha, _oknoIgrovoe;
    public GameObject noCoinsWindow, confirmPurchaseWindow;

    private bool isGameFinished = false; // <-- новый флаг

    void Awake()
    {
        Instance = this;
    }

    public void OnGameFinished(bool win)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FindObjectOfType<my>().SignalGameplayStop();
#endif
        if (isGameFinished) return; // если уже показывали окно, ничего не делаем
        isGameFinished = true;      // блокируем повторные вызовы

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
