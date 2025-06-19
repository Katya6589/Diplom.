using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{

    public static SkinManager Instance;

    public List<GameObject> skins;
    public List<Block> blocks;

    void Awake()
    {
        Instance = this;

        // Установить базовый скин, если ничего не выбрано
        if (!PlayerPrefs.HasKey("current_skin"))
        {
            PlayerPrefs.SetInt("current_skin", 0);
            PlayerPrefs.SetInt("skin_0", 1); // На всякий случай
        }
    }


    void Start()
    {
        int currentSkin = PlayerPrefs.GetInt("current_skin", 0);
        if (currentSkin >= 0)
            ChangeSkin(currentSkin);
    }


    public void ChangeSkin(int index)
    {
        foreach (var block in blocks)
        {
            block.SetSkin(skins[index]);
        }
    }
    
}
