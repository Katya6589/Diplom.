using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Progress : MonoBehaviour
{
    public int Coins; //public тк мы будем обращатьс€€ к ним из другх мест
    public static Progress Instance;

    private void Awake() //метод по типу как старт, он он вызывааетс€ еще быстрее чем старт
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;

            DontDestroyOnLoad(gameObject); //функци€ если ее вызвать и указать объект в скобках, тогда он не будет уничтожтьс€ при переоде между сценами
        }
        else
        {
            Destroy(gameObject);
        }



    }
}
