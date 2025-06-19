using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Time_chet : MonoBehaviour
{
    private int _sec = 0;

    //private TMP_Text _TimerText;
    private TMP_Text _TimerText;
    [SerializeField] private int _delta = 0;
    [SerializeField] private GameObject _oknoProirgisha;
    [SerializeField] private GameObject _oknoIgrovoe;
    // Start is called before the first frame update
    void Start()
    {
        _TimerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        StartCoroutine(ITimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ITimer()
    {
        while (true)
        {

            if (_sec == 20)
            {
                _oknoProirgisha.SetActive(true);
                _oknoIgrovoe.SetActive(false);
                _sec = -1;

            }




            _sec += _delta;
            _TimerText.text = _sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

}
