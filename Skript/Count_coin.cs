using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_coin : MonoBehaviour
{
    public int _numderOfCoins;
    [SerializeField] private TextMeshProUGUI _text;

    public int testCoins;
    
    public static Count_coin Instance;


    void Awake()
    {
        Instance = this;
        
        #if UNITY_EDITOR
        if (testCoins > 0)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + testCoins);
        }
        #endif
    }

    public void Start()
    {
        _numderOfCoins = PlayerPrefs.GetInt("coins", 0);//Progress.Instance.Coins;
        _text.text = _numderOfCoins.ToString();

    }

    public void AddOne()
    {
        _numderOfCoins += 1;
        _text.text = _numderOfCoins.ToString();

    }

    public void AddFive()
    {
        _numderOfCoins += 5;
        _text.text = _numderOfCoins.ToString();

    }


    public void SaveToProgress()
    {
        //Progress.Instance.Coins = _numderOfCoins;
        PlayerPrefs.SetInt("coins", _numderOfCoins);
    }

    public void SpendMoney(int value)
    {
        _numderOfCoins -= value;
        _text.text = _numderOfCoins.ToString();

    }
}
