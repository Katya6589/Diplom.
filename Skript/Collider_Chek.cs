using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Chek : MonoBehaviour
{

    public Peremeshenie_cuba moveScript;
    
    private int collisionCount = 0; // ������� ��������

    void OnTriggerEnter(Collider other)
    {
        collisionCount++; // ����������� ������� �������� ��� ����� ������� ���������� � ������� �������� ����������
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--; // ��������� ������� �������� ��� ������ ������� ���������� �� �������� �������� ����������
    }

    void Update()
    {
        if (collisionCount >= 4)
        {
            UIManager.Instance.OnGameFinished(true);
            moveScript.SetMoveEnabled(false);
        }
    }
}
