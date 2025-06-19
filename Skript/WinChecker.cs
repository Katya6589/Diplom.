using UnityEngine;

public class WinChecker : MonoBehaviour
{
    public WinTrigger[] triggers; // Заполняется через инспектор
    public Peremeshenie_cuba moveScript;
    private bool winDeclared = false;

    void Update()
    {
        if (winDeclared) return;

        bool allOccupied = true;
        foreach (var trigger in triggers)
        {
            if (!trigger.isOccupied)
            {
                allOccupied = false;
                break;
            }
        }

        if (allOccupied)
        {
            winDeclared = true;
            UIManager.Instance.OnGameFinished(true);
            moveScript.SetMoveEnabled(false);
        }
    }
}
