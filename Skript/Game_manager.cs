using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; //метод для сцен

public class Game_manager : MonoBehaviour
{
    //[SerializeField] GameObject _startMenu;
    [SerializeField] TextMeshProUGUI _level;


    //[SerializeField] GameObject _finishWindow;
    [SerializeField] Count_coin _coinManager;

    //[SerializeField] GameObject _okno;


    // Start is called before the first frame update
    void Start()
    {
        _level.text = SceneManager.GetActiveScene().name;
        UIManager.Instance.ResetGameFinished();
#if UNITY_WEBGL && !UNITY_EDITOR
        FindObjectOfType<my>().SignalLoadingReady();
#endif
        //метод для сцен(узнаем имя сцены и передаем в текст
        // _levelTWO.text = SceneManager.GetActiveScene().name;
        // _levelTHREE.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void NextLevel()
    {
        _coinManager.AddFive();
        _coinManager.SaveToProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        /*int next = SceneManager.GetActiveScene().buildIndex + 1;
        int LevelOne = SceneManager.GetActiveScene().buildIndex;//d
        int rers = LevelOne - LevelOne;
        if (next < SceneManager.sceneCountInBuildSettings) // если индекс след сцены меньше общего кол-во сцен, тогда показываем след сцену
        {
            _coinManager.AddOne();
            _coinManager.SaveToProgress();
            SceneManager.LoadScene(next);
           
        }
        if (next == SceneManager.sceneCountInBuildSettings)//d
        {
            SceneManager.LoadScene(rers); //d
        }*/


    }
    public void Restart()
    {
        int res = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(res);
    }

    public void OnRestartButtonClick()
    {
        YandexAdsManager.Instance.ShowFullscreenWithCooldown(() =>
        {
            FindObjectOfType<Game_manager>().Restart();
        });
    }
}
