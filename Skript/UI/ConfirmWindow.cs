using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmWindow : MonoBehaviour
{

    public UnityAction onConfirm;

    public void OnConfirmClick()
    {
        onConfirm?.Invoke();
        gameObject.SetActive(false);
    }

}
